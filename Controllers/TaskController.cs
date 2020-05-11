using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task.Data;
using Task.Models;
using Task.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Task.Controllers
{
    public class TaskController : Controller
    {
        private readonly DataContext _context;

        public TaskController(DataContext context)
        {
            _context = context;
        }
        // GET: /<controller>/


        public IActionResult EditorList()
        { 
            //if(HttpContext.Session.GetString("Email") != null) 
            //{ 
                var test = _context.Userdetails.Where(m=>m.Actor == "Editor").ToList(); 
                var model = new List<Userdetails>(); 
                foreach (var i in test) 
                { 
                    var s = new Userdetails() 
                    { 
                        Id = i.Id, 
                        Name = i.Name,
                    };
                    model.Add(s);
                } 
                return View(model);
            //}
            //else 
            //{ 
            //    return RedirectToAction("Login", "Home");
            //}
        }
        
        public IActionResult TaskType(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        public IActionResult Facebook(int Id)
        {
            List<Userdetails> monitor = new List<Userdetails>();
            monitor = _context.Userdetails.Where(u => u.Actor == "Monitor").ToList();
            monitor.Insert(0, new Userdetails { Id = 0, Name = "Select one" });
            ViewBag.Monitor = monitor;
            ViewBag.Id = Id;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Facebook(SetTaskAdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                ///model validation monitor and task duration 
                ///organize korte hobe fb link insta
                
                //if (model.MonitorId == 0)
                //{
                //    ModelState.AddModelError("MonitorId", "Please Select a Monitor.");
                //    return View();
                //}
                //var compare1 = DateTime.Compare(DateTime.Now, model.TaskDuration);
                //if (compare1 != -1)
                //{
                //    ModelState.AddModelError("TaskDuration", "Invalid DateTime attempt.");
                //    return View(new { Id = model.EditorId });
                //}

                ///

                var z = "";
                var y = ", ";
                var c = 0;
                if (model.AreChecked != null)
                {
                    
                    foreach (var x in model.AreChecked)
                    {
                        c++;

                        if (model.AreChecked.Count == c && model.AddTask == null)
                        {
                            z = z + x;
                        }
                        else
                        {
                            z = z + x;
                            z += y;
                        }
                    }
                }
                TaskList user = new TaskList
                {
                    ScocialMedia = model.SocialMedia,
                    MonitorId = model.MonitorId,
                    EditorId = model.EditorId,
                    TaskSetTime = DateTime.Now,
                    TaskDuration = model.TaskDuration,
                    TaskName =  z + model.AddTask +"."
                };
                var compare = DateTime.Compare(user.TaskSetTime, user.TaskDuration);
                if (compare == -1)
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("TaskType", "Task", new { Id = model.EditorId });
                }
                else
                {
                    ModelState.AddModelError("TaskDuration", "Invalid DateTime attempt.");

                    return RedirectToAction("Facebook", "Task", new { Id = model.EditorId });
                }

            }
            else
            {
                return RedirectToAction("Facebook", "Task", new { Id = model.EditorId });
            }
        }
        public IActionResult Instagram( int Id)
        {
            List<Userdetails> monitor = new List<Userdetails>();
            monitor = _context.Userdetails.Where(u => u.Actor == "Monitor").ToList();
            monitor.Insert(0, new Userdetails { Id = 0, Name = "Select one" });
            ViewBag.Monitor = monitor;
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Instagram(SetTaskAdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                var z = "";
                var y = ", ";
                var c = 0;
                if (model.AreChecked != null)
                {
                    foreach (var x in model.AreChecked)
                    {
                        c++;

                        if (model.AreChecked.Count == c && model.AddTask == null) 
                        {
                            z = z + x;
                        }
                        else
                        {
                            z = z + x;
                            z += y;
                        }
                    }
                }
                TaskList user = new TaskList
                {
                    ScocialMedia = model.SocialMedia,
                    MonitorId = model.MonitorId,
                    EditorId = model.EditorId,
                    TaskSetTime = DateTime.Now,
                    TaskDuration = model.TaskDuration,
                    TaskName = z + model.AddTask + "."
                };
                var compare = DateTime.Compare(user.TaskSetTime, user.TaskDuration);
                if(compare ==-1)
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("TaskType", "Task", new { Id = model.EditorId });
                }
                else
                {
                    ModelState.AddModelError("TaskDuration", "Invalid DateTime attempt.");
                    return RedirectToAction("Instagram", "Task", new { Id = model.EditorId });
                }
            }
            else
            {
                return RedirectToAction("Instagram", "Task", new { Id = model.EditorId });

            }

        }
        public IActionResult LinkedIn(int Id)
        {
            List<Userdetails> monitor = new List<Userdetails>();
            monitor = _context.Userdetails.Where(u => u.Actor == "Monitor").ToList();
            monitor.Insert(0, new Userdetails { Id = 0, Name = "Select one" });
            ViewBag.Monitor = monitor;
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> LinkedIn(SetTaskAdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                var z = "";
                var y = ", ";
                var c = 0;
                if (model.AreChecked != null)
                {
                    foreach (var x in model.AreChecked)
                    {
                        c++;

                        if (model.AreChecked.Count == c && model.AddTask == null)
                        {
                            z = z + x;
                        }
                        else
                        {
                            z = z + x;
                            z += y;
                        }
                    }
                }
                TaskList user = new TaskList
                {
                    ScocialMedia = model.SocialMedia,
                    MonitorId = model.MonitorId,
                    EditorId = model.EditorId,
                    TaskSetTime = DateTime.Now,
                    TaskDuration = model.TaskDuration,
                    TaskName = z + model.AddTask + "."
                };
                var compare = DateTime.Compare(user.TaskSetTime, user.TaskDuration);
                if (compare == -1)
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("TaskType", "Task", new { Id = model.EditorId });
                }
                else
                {
                    ModelState.AddModelError("TaskDuration", "Invalid DateTime attempt.");
                    return RedirectToAction("LinkedIn", "Task", new { Id = model.EditorId });
                }
            }
            else
            {
                return RedirectToAction("LinkedIn", "Task", new { Id = model.EditorId });

            }
        }


        public IActionResult Add()
        {
            List<Userdetails> employee = new List<Userdetails>();
            employee = _context.Userdetails.Where(u => u.Actor == "Employee").ToList();
            employee.Insert(0, new Userdetails { Id = 0, Name = "Select one" });
            ViewBag.Employee = employee;
            List < Userdetails > monitor  = new List<Userdetails>();
            monitor = _context.Userdetails.Where(u => u.Actor == "Monitor").ToList();
            monitor.Insert(0, new Userdetails { Id = 0, Name = "Select one" }) ;
            ViewBag.Monitor = monitor;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Add(SetTaskAdminViewModel model)
        {

            if (ModelState.IsValid)
            {
                TaskList user = new TaskList
                {
                    ScocialMedia = model.SocialMedia,
                    MonitorId = model.MonitorId,
                    EditorId = model.EditorId,
                    TaskSetTime = model.TaskSetTime,
                    TaskDuration = model.TaskDuration,
                    TaskName = model. AddTask
                };
                _context.Add(user);
                await _context.SaveChangesAsync();

            }
            else
            {
                return View("Add");
            }
            return RedirectToAction("Index", "Home");
        }
       
        public IActionResult AllDoneTask()
        {
            
            IQueryable<TaskList> taskLists = from s in _context.TaskList
                                             select s;
            taskLists  = taskLists.OrderByDescending(t => t.TaskId);
            //var test = taskLists.ToList();
            var test = taskLists.Where(m=>m.EditorReview=="Done").ToList();
            
            return View(test);
        }
        public IActionResult AllPandingTask()
        {

            IQueryable<TaskList> taskLists = from s in _context.TaskList
                                             select s;
            taskLists = taskLists.OrderByDescending(t => t.TaskId);
            //var test = taskLists.ToList();
            var test = taskLists.Where(m => m.EditorReview != "Done").ToList();

            return View(test);
        }
        public IActionResult TaskDetails(int Id)
        {
            var data = _context.TaskList.Where(u => u.TaskId == Id).FirstOrDefault();

            var editor = _context.Userdetails.Where(m=> m.Id == data.EditorId).FirstOrDefault();
            var monitor = _context.Userdetails.Where(m=> m.Id == data.MonitorId).FirstOrDefault();
            ViewBag.Editor = editor.Name;
            ViewBag.Monitor = monitor.Name;
            
            return View(data);
        }
    }
}
