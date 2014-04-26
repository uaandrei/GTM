using ModelTask = Tasks.Model.Task;
using GoogleTask = Google.Apis.Tasks.v1.Data.Task;

namespace GoogleTasksService.Adapters
{
    internal static class TaskAdapter
    {
        public static ModelTask ToModelTask(GoogleTask task)
        {
            var isDone = task.Status == GoogleApiConstants.CompletedStatus;
            return new ModelTask(task.Id, task.Title, task.Notes, isDone, task.Due);
        }

        public static GoogleTask ToGoogleTask(ModelTask task)
        {
            return new GoogleTask
            {
                Id = task.GoogleId,
                Title = task.Title,
                Notes = task.Description,
                Status = task.IsDone ? GoogleApiConstants.CompletedStatus : GoogleApiConstants.NeedsActionStatus,
                Due = task.Due
            };
        }
    }
}
