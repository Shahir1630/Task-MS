using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task.Data;
using Task.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Task.Controllers
{
    public class EditorController : Controller
    {
        // GET: /<controller>/
        private readonly DataContext _context;

        public EditorController(DataContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index(string Email)
        {
            var userdetails = await _context.Userdetails
                .FirstOrDefaultAsync(m => m.Email == Email);

            var editorTask = await _context.TaskList.Where(m => m.EditorId == userdetails.Id && m.EditorReview != "Done").ToListAsync();
            if (editorTask.Count == 0)
            {
                return RedirectToAction("NoTask", "Home");
            }

            ViewBag.Email = userdetails.Email;
            return View(editorTask);
        }
        public IActionResult TaskDetails(int Id, string Email)
        {
            var model = _context.TaskList.Where(u => u.TaskId == Id).FirstOrDefault();
            ViewBag.Email = Email;
            return View(model);
        }
        [HttpPost]
        public IActionResult TaskDetails(TaskList model, string Email)
        {
            var data = _context.TaskList.Where(u => u.TaskId == model.TaskId).FirstOrDefault();

            data.EditorReview = model.EditorReview;
            data.TaskFinished = DateTime.Now;
            //new
            var compare = DateTime.Compare(model.TaskDuration, data.TaskFinished);
            if (compare != -1)
            {
                var z = data;
                _context.SaveChanges();
                return RedirectToAction("Index", "Editor", new { Email = Email });
            } 
            else
            {
                return RedirectToAction("TimeOver", "Editor");
            }
            //var z = data;
            //_context.SaveChanges();
            //return RedirectToAction("Index", "Editor", new { Email = Email });
        }
        public IActionResult TimeOver()
        {
            return View();
        }
    }
}
