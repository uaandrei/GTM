using GoogleTasksService;
using System.Threading.Tasks;
using Tasks.Persistence;

namespace GoogleTasksManager.Synchronization
{
    public static class SynchronizationManager
    {
        public static async Task SyncWithGoogle(this TaskContainer taskContainer)
        {
            var service = new GoogleTaskService();
            var taskLists = await service.GetTaskLists();
            foreach (var taskList in taskLists)
            {
                taskContainer.AddUpdateTaskList(taskList);
                var tasks = await service.GetTasksForTaskList(taskList);
                foreach (var task in tasks)
                {
                    taskContainer.AddUpdateTask(task, taskList.Id);
                }
            }
        }
    }
}
