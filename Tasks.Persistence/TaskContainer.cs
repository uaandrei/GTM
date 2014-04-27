using System;
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

        public void SaveTask(Task task, int taskListId)
        {
            using (var context = GetContext())
            {
                DbTask dbTask;
                if (string.IsNullOrEmpty(task.GoogleId))
                {
                    dbTask = context.Tasks.FirstOrDefault(p => p.Id == task.DbId);
                }
                else
                {
                    dbTask = context.Tasks.FirstOrDefault(p => p.Uid == task.GoogleId);
                }
                if (dbTask == null)
                {
                    dbTask = TaskAdapter.ToDbTask(task);
                    dbTask.TaskListId = taskListId;
                    context.Tasks.InsertOnSubmit(dbTask);
                }
                else
                {
                    dbTask.Description = task.Description;
                    dbTask.Due = task.Due;
                    dbTask.IsDone = task.IsDone;
                    dbTask.Title = task.Title;
                    dbTask.TaskListId = taskListId;
                }
                context.SubmitChanges();
                task.DbId = dbTask.Id;
            }
        }

        public void SaveTaskList(TaskList taskList)
        {
            using (var context = GetContext())
            {
                DbTaskList dbTaskList;
                if (string.IsNullOrEmpty(taskList.GoogleId))
                {
                    dbTaskList = context.TaskLists.FirstOrDefault(p => p.Id == taskList.DbId);
                }
                else
                {
                    dbTaskList = context.TaskLists.FirstOrDefault(p => p.Uid == taskList.GoogleId);
                }
                if (dbTaskList == null)
                {
                    dbTaskList = TaskListAdapter.ToDbTaskList(taskList);
                    context.TaskLists.InsertOnSubmit(dbTaskList);
                }
                else
                {
                    dbTaskList.Name = taskList.Name;
                }
                context.SubmitChanges();
                taskList.DbId = dbTaskList.Id;
            }
        }

        public void UpdateTask(Task task)
        {
            using (var context = GetContext())
            {
                var existingTask = context.Tasks.First(p => p.Id == task.DbId);
                existingTask.Description = task.Description;
                existingTask.Due = task.Due;
                existingTask.IsDone = task.IsDone;
                existingTask.Title = task.Title;
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

        public List<Task> GetTasksForTaskList(int id)
        {
            using (var context = GetContext())
            {
                return context.Tasks.Where(p => p.TaskListId == id).Select(TaskAdapter.ToTask).ToList();
            }
        }

        public Task GetTask(int id)
        {
            using (var context = GetContext())
            {
                return TaskAdapter.ToTask(context.Tasks.First(p => p.Id == id));
            }
        }

        public TaskList GetTaskList(int id)
        {
            using (var context = GetContext())
            {
                return TaskListAdapter.ToTaskList(context.TaskLists.First(p => p.Id == id));
            }
        }

        private TaskDataContext GetContext()
        {
            return new TaskDataContext(TaskDataContext.DBConnectionString);
        }

        public void UpdateGoogleIdForTask(Task task, string newGoogleId)
        {
            using (var context = GetContext())
            {
                var dbTask = context.Tasks.First(p => p.Id == task.DbId);
                dbTask.Uid = newGoogleId;
                context.SubmitChanges();
            }
        }

        public bool TaskExists(string googleId)
        {
            using (var context = GetContext())
            {
                return context.Tasks.FirstOrDefault(p => p.Uid == googleId) != null;
            }
        }

        public bool TaskExists(int id)
        {
            using (var context = GetContext())
            {
                return context.Tasks.FirstOrDefault(p => p.Id == id) != null;
            }
        }

        public void AddTaskToTaskList(Task task, int taskListDbId)
        {
            using (var context = GetContext())
            {
                var dbTask = TaskAdapter.ToDbTask(task);
                dbTask.TaskListId = taskListDbId;
                context.Tasks.InsertOnSubmit(dbTask);
                context.SubmitChanges();
            }
        }

        public void DeleteTask(int id)
        {
            using (var context = GetContext())
            {
                var dbTask = context.Tasks.First(p => p.Id == id);
                context.Tasks.DeleteOnSubmit(dbTask);
                context.SubmitChanges();
            }
        }
    }
}