using System.Data.Entity;

namespace EventHubSampleSendData
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Attendance> GF_attendance { get; set; }

        public ApplicationContext()
        {
            //There may be auxiliary logic when initializing the database context, as well as to see the supported databases.
            //https://docs.microsoft.com/en-us/ef/#:~:text=Entity%20Framework%20Core%20is%20a%20modern%20object-database%20mapper,Azure%29%2C%20SQLite%2C%20MySQL%2C%20PostgreSQL%2C%20and%20Azure%20Cosmos%20DB.
        }
    }
}
