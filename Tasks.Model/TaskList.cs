using System.Collections.Generic;

namespace Tasks.Model
{
    public class TaskList
    {
        public string Name { get; set; }

        public string GoogleId { get; set; }

        public int DbId { get; set; }

        public TaskList(string name, string id)
        {
            Name = name;
            GoogleId = id;
        }
    }
}
