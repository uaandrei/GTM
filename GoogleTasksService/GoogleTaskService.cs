using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Tasks.v1;
using GoogleTasksService.Adapters;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tasks.Model;
using ModelTask = Tasks.Model.Task;

namespace GoogleTasksService
{
    public class GoogleTaskService
    {
        private TasksService _service;

        public GoogleTaskService()
        {
            Initialize();
        }

        private async void Initialize()
        {
            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read),
                new[] { TasksService.Scope.Tasks },
                "user",
                CancellationToken.None);
            _service = new TasksService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "GTM"
            });
        }

        public async Task<List<TaskList>> GetTaskLists()
        {
            var taskLists = await _service.Tasklists.List().ExecuteAsync();
            return taskLists.Items.Select(TaskListAdapter.ToModelTaskList).ToList();
        }

        public async Task<List<ModelTask>> GetTasksForTaskList(TaskList taskList)
        {
            var tasks = await _service.Tasks.List(taskList.Id).ExecuteAsync();
            return tasks.Items.Select(TaskAdapter.ToModelTask).ToList();
        }

        ~GoogleTaskService()
        {
            _service.Dispose();
        }
    }
}
