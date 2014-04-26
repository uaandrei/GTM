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

        public void AddUpdateTaskList(TaskList taskList)
        {
            using (var context = GetContext())
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
                context.SubmitChanges();
            }
        }

        public void AddUpdateTask(Task task, string taskListId)
        {
            using (var context = GetContext())
            {
                var taskList = context.TaskLists.First(p => p.Uid == taskListId);
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
                context.SubmitChanges();
            }
        }

        public List<TaskList> GetAllTaskLists()
        {
            using (var context = GetContext())
            {
                return context.TaskLists.Select(TaskListAdapter.ToTaskList).ToList();
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

        public Task GetTask(string taskId)
        {
            using (var context = GetContext())
            {
                return TaskAdapter.ToTask(context.Tasks.First(p => p.Uid == taskId));
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

        public void SaveTask(Task task)
        {
            using (var context = GetContext())
            {
                var existingTask = context.Tasks.FirstOrDefault(p => p.Uid == task.Id);
                if (existingTask == null)
                {
                    context.Tasks.InsertOnSubmit(TaskAdapter.ToDbTask(task));
                }
                else
                {
                    existingTask.Title = task.Title;
                    existingTask.Description = task.Description;
                }
                context.SubmitChanges();
            }
        }
    }
}