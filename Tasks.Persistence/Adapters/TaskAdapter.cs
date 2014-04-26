using Tasks.Model;
namespace Tasks.Persistence.Adapters
{
    internal static class TaskAdapter
    {
        public static DbTask ToDbTask(Task task)
        {
            return new DbTask
            {
                Description = task.Description,
                Due = task.Due,
                IsDone = task.IsDone,
                Title = task.Title,
                Uid = task.GoogleId
            };
        }

        public static Task ToTask(DbTask task)
        {
            return new Task(task.Uid, task.Title, task.Description, task.IsDone, task.Due)
            {
                DbId = task.Id
            };
        }
    }
}
