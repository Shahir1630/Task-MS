using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task.Data;
using Task.Models;
using Task.ViewModels;

namespace Task.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;
        public HomeController(DataContext context)
        {
            _context = context;
        }

        public IActionResult NoTask()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Welcome(string Email)
        {
            ViewBag.Email = Email;
            return View();
        }
        
        public IActionResult AllEmployee()
        {
            var test = _context.Userdetails.ToList();
            var model = new List<Userdetails>();
            foreach(var i in test)
            {
                var s = new Userdetails()
                {
                    Id = i.Id,
                    Name = i.Name,
                    Mobile = i.Mobile,
                    Email = i.Email,
                    Actor = i.Actor
                };
                model.Add(s);
            }
            
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userdetails = await _context.Userdetails
                .FirstOrDefaultAsync(m => m.Email == model.Email && m.Password == model.Password && m.Actor == model.Actor);
                if (userdetails == null)
                {
                    ModelState.AddModelError("Email", "Invalid login attempt.");
                    ModelState.AddModelError("Password", "Invalid login attempt.");
                    ModelState.AddModelError("Actor", "Invalid login attempt.");
                    return View();
                }
                HttpContext.Session.SetString("userId", userdetails.Name);
                HttpContext.Session.SetString("actorId", userdetails.Actor);
                HttpContext.Session.SetString("email", userdetails.Email);


                if (userdetails.Actor == "Monitor")
                    return RedirectToAction("Welcome", "Home", new { Email = model.Email });
                else if(userdetails.Actor == "Editor")
                    return RedirectToAction("Welcome", "Home", new { Email = model.Email });
            }
            else
            {
                return View("Login");
            }
            return RedirectToAction("Welcome", "Home",new {Email = model.Email });
        }
        [HttpPost]
        public async Task<ActionResult> Registration(RegistrationViewModel model)
        {

            var Name = model.FirstName + " " + model.LastName;
            /// Email validation
            var email = _context.Userdetails.Where(m => m.Email == model.Email).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (email != null)
                {
                    ModelState.AddModelError("Email", " This email already used.");
                    return View();
                }
                
                Userdetails user = new Userdetails
                {
                    Name =Name,
                    Email = model.Email,
                    Password = model.Password,
                    Mobile = model.Mobile,
                    Actor = model.Actor

                };
                _context.Add(user);
                await _context.SaveChangesAsync();

            }
            else
            {
                return View("Registration");
            }
            return RedirectToAction("SuccessMessage", "Home");
        }
        // registration Page load
        public IActionResult Registration()
        {
            ViewData["Message"] = "Registration Page";

            return View();
        }
        public IActionResult SuccessMessage()
        {
            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }
        public void ValidationMessage(string key, string alert, string value)
        {
            try
            {
                TempData.Remove(key);
                TempData.Add(key, value);
                TempData.Add("alertType", alert);
            }
            catch
            {
                Debug.WriteLine("TempDataMessage Error");
            }

        }

        public IActionResult Edit(int Id)
        {
            var model = _context.Userdetails.Where(u => u.Id == Id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit( Userdetails model)
        {
            var data = _context.Userdetails.Where(u => u.Id == model.Id).FirstOrDefault();
            data.Mobile = model.Mobile;
            data.Name = model.Name;
            data.Email = model.Email;
            data.Password = model.Password;
            data.Actor = model.Actor;

            _context.SaveChanges();
            return RedirectToAction("AllEmployee");
        }

        public IActionResult Delete(int Id)
        {
            var model = _context.Userdetails.Where(u => u.Id == Id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(Userdetails model)
        {
            var data = _context.Userdetails.Where(u => u.Id == model.Id).FirstOrDefault();
            _context.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("AllEmployee");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
