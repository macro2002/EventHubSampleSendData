using Azure.Messaging.EventHubs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs.Producer;
using System.Timers;
using EventHubSampleSendData;
using Newtonsoft.Json;

namespace SendSampleData
{
    class Program
    {
        const string eventHubName = "test-hub";
        const string connectionString = @"<YourConnectionString>";
        private static DateTime lastDate;
        private static Timer aTimer;

        public static void Main(string[] args)
        {
            Initialization(DateTime.Now);
        }


        //We send the last date value of the last saved data or you can store it in the application configuration.
        private static void Initialization(DateTime date)
        {
            lastDate = date;
            SetTimer();
        }

        private static void SetTimer()
        {
            // Create a timer with a 30 seconds interval.
            aTimer = new Timer(30000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static void StartTimer()
        {
            aTimer.Stop();
            aTimer.Dispose();
        }


        public static async Task EventHubIngestionAsync()
        {
            await using (var producerClient = new EventHubProducerClient(connectionString, eventHubName))
            {
                using(var db = new ApplicationContext())
                {
                    foreach(var data in db.GF_attendance.Where(d => d.Dateadd > lastDate))
                    {
                        string recordString = string.Join(Environment.NewLine, JsonConvert.SerializeObject(data));
                        EventData eventData = new EventData(Encoding.UTF8.GetBytes(recordString));

                        // optional argument
                        //eventData.Properties.Add("Table", "TestTable");
                        //eventData.Properties.Add("IngestionMappingReference", "TestMapping");
                        //eventData.Properties.Add("Format", "json");

                        using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();
                        eventBatch.TryAdd(eventData);

                        lastDate = data.Dateadd;
                        await producerClient.SendAsync(eventBatch);
                    }
                }
            }
        }

        private static async void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            await EventHubIngestionAsync();
        }
    }
}
