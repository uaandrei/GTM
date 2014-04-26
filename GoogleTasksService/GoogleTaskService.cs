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
        private const string User = "user";
        private CancellationToken CancellationToken = CancellationToken.None;
        private IEnumerable<string> _scopes;

        public GoogleTaskService()
        {
            _scopes = new[] { TasksService.Scope.Tasks };
        }

        private async Task<TasksService> GetTaskService()
        {
            using (var secretStream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    secretStream,
                    _scopes,
                    User,
                    CancellationToken);

                return new TasksService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "GTM"
                });
            }
        }

        public async Task<List<TaskList>> GetTaskLists()
        {
            using (var service = await GetTaskService())
            {
                var taskLists = await service.Tasklists.List().ExecuteAsync();
                return taskLists.Items.Select(TaskListAdapter.ToModelTaskList).ToList();
            }
        }

        public async Task<List<ModelTask>> GetTasksForTaskList(TaskList taskList)
        {
            using (var service = await GetTaskService())
            {
                var tasks = await service.Tasks.List(taskList.GoogleId).ExecuteAsync();
                return tasks.Items.Select(TaskAdapter.ToModelTask).ToList();
            }
        }

        public async Task<ModelTask> AddTaskToTaskList(ModelTask task, TaskList taskList)
        {
            using (var service = await GetTaskService())
            {
                var addedTask = await service.Tasks.Insert(TaskAdapter.ToGoogleTask(task), taskList.GoogleId).ExecuteAsync();
                return TaskAdapter.ToModelTask(addedTask);
            }
        }
    }
}
