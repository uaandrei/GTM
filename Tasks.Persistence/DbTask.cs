using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Tasks.Persistence
{
    [Table(Name = "Tasks")]
    internal class DbTask
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id { get; set; }

        [Column]
        public string Uid { get; set; }

        [Column]
        public int TaskListId { get; set; }

        [Column]
        public string Title { get; set; }

        [Column]
        public string Description { get; set; }

        [Column]
        public bool IsDone { get; set; }

        [Column(CanBeNull = true)]
        public DateTime? Due { get; set; }

        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;
    }
}
