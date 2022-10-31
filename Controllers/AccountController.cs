using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Context;
using WebApp.ViewModel;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        MyContext myContext;

        public AccountController(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        //-- Login --//
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            var data = myContext.Users
                  .Include(x => x.Employee)
                  .Include(x => x.Role)
                  .SingleOrDefault(x => x.Employee.Email.Equals(email) && x.Password.Equals(password));
            if (data != null)
            {
                /*User user = new User()
                {
                    Password = data.Password
                };*/
                ResponseLogin responseLogin = new ResponseLogin()
                {
                    FullName = data.Employee.FullName,
                    Email = data.Employee.Email,
                    Role = data.Role.Name
                };
                return RedirectToAction("Index", "Home", responseLogin);
                ///respone login di tambah di view home @model dan controller home

            }
            return View();

        }

        ///-- Register --///
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string fullname, string email, DateTime birthDate, string password)
        {
            Employee employee = new Employee()
            {
                FullName = fullname,
                Email = email,
                BirthDate = birthDate

            };
            myContext.Employees.Add(employee);
            var result = myContext.SaveChanges();
            if (result > 0)
            {
                var id = myContext.Employees.SingleOrDefault(x => x.Email.Equals(email)).Id;
                User user = new User()
                {
                    Id = id,
                    Password = password,
                    RoleId = 1
                };
                myContext.Users.Add(user);
                var resultUser = myContext.SaveChanges();
                if (resultUser > 0)

                    return RedirectToAction("Login", "Account");


            }
            return View();
        }

        /// -- Change Password -- ///

        public IActionResult ChangePassword()
        {

            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(string PasswordLama, User user)
        {
            var data = myContext.Users.SingleOrDefault(x => x.Password.Equals(PasswordLama));

            if (data != null)
            {
                if (data.Password == PasswordLama)
                {
                    data.Password = user.Password;
                    myContext.Entry(data).State = EntityState.Modified;
                    var result = myContext.SaveChanges();
                    if (result > 0)
                        return RedirectToAction("Login");
                }
            }

            return View();
        }

        /// -- Forgot Passsword --///

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]

        public IActionResult ForgotPassword(string FullName, string Email, string NewPass)
        {

             var data = myContext.Users
             .Include(x => x.Employee)
            
             .SingleOrDefault(x => x.Employee.Email.Equals(Email) && x.Employee.FullName.Equals(FullName));
              if (data != null)
              {
                data.Password = NewPass;
                myContext.Entry(data).State = EntityState.Modified;
                var result = myContext.SaveChanges();
                if (result > 0)
                {
                    return RedirectToAction("Login", "Account");
                }
           }


              return View();

            /*public IActionResult ForgotPassword(string FullName, string Email)
            {
                // mencari email seusai id
                var dataId = myContext.Employees.SingleOrDefault(x => x.Email.Equals(Email)).Id;
                var data = myContext.Employees.Find(dataId);

                if ((data.FullName == FullName))
                {
                    var users = myContext.Users.Find(dataId);
                    return View(users);
                }
                return View();
*/

            }

        /// --- New Password --

        public IActionResult NewPass()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewPass(string passBaru, Employee employee, User user)
        {
            var data = myContext.Users.Find(employee.Email);
            if (data != null)
            {
                data.Password = user.Password;
                
                myContext.Entry(data).State = EntityState.Modified;
                var result = myContext.SaveChanges();
                if (result > 0)
                    return RedirectToAction("Index", "Home");
            }

            return View();
        }

    }
    

}

