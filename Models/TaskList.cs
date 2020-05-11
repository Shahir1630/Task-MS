using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task.Models
{
    public class TaskList
    {
        [Key]
        public int TaskId { get; set; }
        public string ScocialMedia { get; set; }
        public string MonitorReview { get; set; }
        public int EditorId { get; set; }
        public int MonitorId { get; set; }
        public DateTime TaskSetTime { get; set; }
        public DateTime TaskDuration { get; set; } 
        public DateTime TaskFinished { get; set; }
        public string EditorReview  { get; set; }
        public string TaskName { get; set; }
    }
}
