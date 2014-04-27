using System.Linq;
using GoogleTasksService;
using System.Threading.Tasks;
using Tasks.Persistence;

namespace GoogleTasksManager.Synchronization
{
    public static class SynchronizationManager
    {
        public static async Task SyncWithGoogle(this TaskContainer taskContainer)
        {
            await SyncToGoogle(taskContainer);
            await SyncFromGoogle(taskContainer);
        }

        private static async Task SyncToGoogle(TaskContainer taskContainer)
        {
            var service = new GoogleTaskService();
            foreach (var taskList in taskContainer.GetAllTaskLists())
            {
                foreach (var task in taskContainer.GetTasksForTaskList(taskList.DbId).Where(p => string.IsNullOrEmpty(p.GoogleId)))
                {
                    var addedTask = await service.AddTaskToTaskList(task, taskList);
                    taskContainer.UpdateGoogleIdForTask(task, addedTask.GoogleId);
                }
            }
        }

        private static async Task SyncFromGoogle(TaskContainer taskContainer)
        {
            var service = new GoogleTaskService();
            foreach (var taskList in await service.GetTaskLists())
            {
                taskContainer.SaveTaskList(taskList);
                var googleTasks = await service.GetTasksForTaskList(taskList);
                foreach (var task in googleTasks)
                {
                    taskContainer.SaveTask(task, taskList.DbId);
                }
                foreach (var task in taskContainer.GetTasksForTaskList(taskList.DbId))
                {
                    if (googleTasks.Find(p => p.GoogleId == task.GoogleId) == null)
                    {
                        taskContainer.DeleteTask(task.DbId);
                    }
                }
            }
        }
    }
}
