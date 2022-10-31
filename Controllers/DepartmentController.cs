using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Context;
using WebApp.Models;


namespace WebApp.Controllers
{
    public class DepartmentController : Controller
    {
        MyContext myContext;
        public DepartmentController(MyContext myContext)
        {
            this.myContext = myContext;
        }


        /// --- GET ALL ---
        public IActionResult Index()
        {
            var data = myContext.Departments.ToList();
            
            return View(data);
        }

        /// --- GET ID ---
        
        public IActionResult Details (int id,Department department)
        {
            
            var data = myContext.Departments.Find(id);
            data.DivisionId = department.DivisionId;
            return View(data);
        }

        /// --- CREATE - GET POST ---
        
        public IActionResult Create ()
        {
            //// MyViewModel menampung nama,id,devision bisa break down

            var vm = new MyViewModel();
            vm.Divisions = myContext.Divisions.Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToList();
            

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public IActionResult Create(Department department)
        {
            myContext.Departments.Add(department);
            var result = myContext.SaveChanges();   
            if (result > 0)
            {
                return RedirectToAction("Index","Department");
            }
            return View();
        }

        /// --- EDIT - GET POST ---
        
        public IActionResult Edit (int id)
        {
            
            var data = myContext.Departments.Find(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit (int id, Department department)
        {
            var data = myContext.Departments.Find(id);
            if (data != null)
            {
                data.Name = department.Name;
                data.DivisionId = department.DivisionId;
                var result = myContext.SaveChanges();
                if (result > 0)
                {
                    return RedirectToAction("Index", "Department");
                }
            }
            return View();
        }

        /// --- DELETE - GET POST ---
        public IActionResult Delete (int id)
        {
            var data = myContext.Departments.Find(id);
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete (int id , Department department)
        {
            myContext.Departments.Remove(department);
            var result = myContext.SaveChanges();
            if (result > 0)
            {
                return RedirectToAction("Index", "Department");
            }
            return View();
        }

    }
}
