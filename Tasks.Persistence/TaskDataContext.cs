using Microsoft.Phone.Data.Linq;
using System.Data.Linq;

namespace Tasks.Persistence
{
    internal class TaskDataContext : DataContext
    {
        public static string DBConnectionString = "Data Source=isostore:/GTM.sdf";
        private static int DbVersion = 1;

        public Table<DbTaskList> TaskLists;
        public Table<DbTask> Tasks;

        public TaskDataContext(string connectionString)
            : base(connectionString)
        {
        }

        internal static void Initialize()
        {
            using (var context = new TaskDataContext(DBConnectionString))
            {
                if (context.DatabaseExists() == false)
                {
                    context.CreateDatabase();
                    var dbUpdater = context.CreateDatabaseSchemaUpdater();
                    dbUpdater.DatabaseSchemaVersion = DbVersion;
                    dbUpdater.Execute();
                }
            }
        }
    }
}
