using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Task.Models;

namespace Task.ViewModels
{
    public class SetTaskAdminViewModel
    {
        public int TaskId { get; set; }
        public string SocialMedia { get; set; }        
        public DateTime TaskSetTime { get; set; }
        [Required] 
        public DateTime TaskDuration { get; set; }        
        public int EditorId { get; set; }
        [Required]
        public int MonitorId { get; set; }
        public string AddTask { get; set; }
        [BindProperty]
        public List<string> AreChecked { get; set;}    

    }
}
