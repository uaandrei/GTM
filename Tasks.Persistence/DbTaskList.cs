using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Tasks.Persistence
{
    [Table(Name = "TaskLists")]
    internal class DbTaskList
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id { get; set; }

        [Column]
        public string Uid { get; set; }

        [Column]
        public string Name { get; set; }

        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;
    }
}