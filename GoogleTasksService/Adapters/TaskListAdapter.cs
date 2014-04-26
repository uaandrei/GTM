using ModelTaskList = Tasks.Model.TaskList;
using GoogleTaskList = Google.Apis.Tasks.v1.Data.TaskList;

namespace GoogleTasksService.Adapters
{
    internal static class TaskListAdapter
    {
        public static ModelTaskList ToModelTaskList(GoogleTaskList taskList)
        {
            return new ModelTaskList(taskList.Title, taskList.Id);
        }

        public static GoogleTaskList ToGoogleTaskList(ModelTaskList taskList)
        {
            return new GoogleTaskList
            {
                Id = taskList.GoogleId,
                Title = taskList.Name
            };
        }
    }
}
