using Microsoft.AspNetCore.Mvc;
using WebApp.Context;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class DivisionController : Controller
    {
        MyContext myContext;
        public DivisionController (MyContext myContext)
        {
            this.myContext = myContext;
        }

        /// --- GET ALL ---
        public IActionResult Index()
        {
            var data = myContext.Divisions.ToList();
            return View(data);
        }

        ///--- GET ID ---
        public IActionResult Details(int id)
        {
            var data = myContext.Divisions.Find(id);
            return View(data);
        }

        /// --- INSERT/CREATE - GET POST ---
       
        public IActionResult Create()
        {
            // get data disini
            // ex: dropdown data didapat dari database
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Division division)
        {
            myContext.Divisions.Add(division);
            var result = myContext.SaveChanges();
            if (result > 0)
            {
                return RedirectToAction("Index", "Division");
            }
            return View();
        }


        /// --- Edit - GET POST ---
        public IActionResult Edit(int id)
        {
            var data = myContext.Divisions.Find(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit (int id, Division division)
        {
            var data = myContext.Divisions.Find(id);
            if (data != null)
            {
                data.Name = division.Name;
                myContext.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                var result = myContext.SaveChanges();
                if (result > 0)
                {
                    return RedirectToAction("Index", "Division");
                }

            }
            return View();
        }

        /// -- DELETE --- GET POST ---
        public IActionResult Delete(int id)
        {
            var data = myContext.Divisions.Find(id);
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete (int id, Division division)
        {
            myContext.Divisions.Remove(division);
            var result = myContext.SaveChanges();
            if (result > 0)
            {
                return RedirectToAction("Index", "Division");
            }
            return View();
        }
    }
}
