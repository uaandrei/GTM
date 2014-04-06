using System.Collections.Generic;

namespace Tasks.Model
{
    public class TaskList
    {
        public string Name { get; set; }

        public string Id { get; set; }

        public TaskList(string name, string id)
        {
            Name = name;
            Id = id;
        }
    }
}
