using System.Collections.Generic;
using System.Linq;
using Tasks.Model;
using Tasks.Persistence.Adapters;

namespace Tasks.Persistence
{
    public class TaskContainer
    {
        public TaskContainer()
        {
            TaskDataContext.Initialize();
        }

        public List<TaskList> GetAllTaskLists()
        {
            using (var context = GetContext())
            {
                return context.TaskLists.Select(TaskListAdapter.ToTaskList).ToList();
            }
        }

        public void Save(TaskList tasklist)
        {
            using (var context = GetContext())
            {
                context.TaskLists.InsertOnSubmit(TaskListAdapter.ToDbTaskList(tasklist));
                context.SubmitChanges();
            }
        }

        public List<Task> GetTasksForTaskList(string taskListId)
        {
            using (var context = GetContext())
            {
                var taskList = context.TaskLists.First(p => p.Uid == taskListId);
                return context.Tasks.Where(p => p.TaskListId == taskList.Id).Select(TaskAdapter.ToTask).ToList();
            }
        }

        public void SyncTaskLists(IList<TaskList> taskLists)
        {
            using (var context = GetContext())
            {
                foreach (var taskList in taskLists)
                {
                    var existingTaskList = context.TaskLists.FirstOrDefault(p => p.Uid == taskList.Id);
                    if (existingTaskList == null)
                    {
                        context.TaskLists.InsertOnSubmit(TaskListAdapter.ToDbTaskList(taskList));
                    }
                    else
                    {
                        existingTaskList.Name = taskList.Name;
                    }
                }
                context.SubmitChanges();
            }
        }

        public void SyncTasksWithTaskList(List<Task> tasks, string taskListId)
        {
            using (var context = GetContext())
            {
                var taskList = context.TaskLists.First(p => p.Uid == taskListId);
                foreach (var task in tasks)
                {
                    var existingTask = context.Tasks.FirstOrDefault(p => p.Uid == task.Id);
                    if (existingTask == null)
                    {
                        var dbTaskToInsert = TaskAdapter.ToDbTask(task);
                        dbTaskToInsert.TaskListId = taskList.Id;
                        context.Tasks.InsertOnSubmit(dbTaskToInsert);
                    }
                    else
                    {
                        existingTask.Description = task.Description;
                        existingTask.Due = task.Due;
                        existingTask.IsDone = task.IsDone;
                        existingTask.Title = task.Title;
                    }
                }
                context.SubmitChanges();
            }
        }

        public TaskList GetTaskList(string taskListId)
        {
            using (var context = GetContext())
            {
                return TaskListAdapter.ToTaskList(context.TaskLists.First(p => p.Uid == taskListId));
            }
        }

        private TaskDataContext GetContext()
        {
            return new TaskDataContext(TaskDataContext.DBConnectionString);
        }
    }
}