using GoogleTasksService;
using System.Threading.Tasks;
using Tasks.Persistence;

namespace GoogleTasksManager.Synchronization
{
    public static class SynchronizationManager
    {
        public static async Task SyncWithGoogle(this TaskContainer taskContainer)
        {
            await SyncFromGoogle(taskContainer);
            await SyncToGoogle(taskContainer);
        }

        private static async Task SyncToGoogle(TaskContainer taskContainer)
        {
            var service = new GoogleTaskService();
            foreach (var taskList in taskContainer.GetAllTaskLists())
            {
                foreach (var task in taskContainer.GetTasksForTaskList(taskList.GoogleId))
                {
                    if (string.IsNullOrEmpty(task.GoogleId))
                    {
                        var addedTask = await service.AddTaskToTaskList(task, taskList);
                        taskContainer.UpdateGoogleIdForTask(task, addedTask.GoogleId);
                    }
                }
            }
        }

        private static async Task SyncFromGoogle(TaskContainer taskContainer)
        {
            var service = new GoogleTaskService();
            foreach (var taskList in await service.GetTaskLists())
            {
                taskContainer.SaveTaskList(taskList);
                foreach (var task in await service.GetTasksForTaskList(taskList))
                {
                    taskContainer.SaveTask(task, taskList.GoogleId);
                }
            }
        }
    }
}
