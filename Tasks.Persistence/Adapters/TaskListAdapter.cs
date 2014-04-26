using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Model;

namespace Tasks.Persistence.Adapters
{
    internal static class TaskListAdapter
    {
        public static DbTaskList ToDbTaskList(TaskList tasklist)
        {
            return new DbTaskList
            {
                Name = tasklist.Name,
                Uid = tasklist.GoogleId
            };
        }

        public static TaskList ToTaskList(DbTaskList tasklist)
        {
            return new TaskList(tasklist.Name, tasklist.Uid)
            {
                DbId = tasklist.Id
            };
        }
    }
}
