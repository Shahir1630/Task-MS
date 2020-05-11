using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task.Data;
using Task.Models;

namespace Task.Controllers
{
    public class MonitorController : Controller
    {

        private readonly DataContext _context;

        public MonitorController(DataContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index(string Email)
        {
            var userdetails = await _context.Userdetails
                .SingleOrDefaultAsync(m => m.Email == Email);

           var monitortask =await _context.TaskList.Where(m=> m.MonitorId == userdetails.Id && m.MonitorReview == null && m.EditorReview =="Done").ToListAsync();
            if (monitortask.Count == 0)
            {
                return RedirectToAction("NoTask", "Home");
            }

            ViewBag.Email = userdetails.Email;
            return View(monitortask);
        }
        public IActionResult TaskDetails(int Id, string Email)
        {
            var model = _context.TaskList.Where(u => u.TaskId == Id).FirstOrDefault();
            ViewBag.Email = Email;

            return View(model);
        }
        [HttpPost]
        public IActionResult TaskDetails(TaskList model , string Email)
        {
            var data = _context.TaskList.Where(u => u.TaskId == model.TaskId).FirstOrDefault();
           
            data.MonitorReview = model.MonitorReview;
            var z = data;
            _context.SaveChanges();
            return RedirectToAction("Index", "Monitor", new { Email = Email });
        }
    }
}