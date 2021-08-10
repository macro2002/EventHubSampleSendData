using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventHubSampleSendData
{
    public class Attendance
    {
        [Column("evstatus")]
        public int Status { get; set; }
        [Column("gateno")]
        public int Gateno { get; set; }
        [Column("eventcode")]
        public string Eventcode { get; set; }
        [Column("section")]
        public string Section { get; set; }
        [Column("barcode")]
        public string Barcode { get; set; }
        [Column("evdatetime")]
        public DateTime Dateadd { get; set; }
        [Column("seat")]
        public string Seat { get; set; }
        [Column("row")]
        public string Row { get; set; }

    }
}
