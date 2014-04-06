using System;

namespace Tasks.Model
{
    public class Task
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
        public DateTime? Due { get; set; }

        public Task(string id, string title, string description, bool isDone, DateTime? due)
        {
            Id = id;
            Title = title;
            Description = description;
            IsDone = isDone;
            Due = due;
        }
    }
}
