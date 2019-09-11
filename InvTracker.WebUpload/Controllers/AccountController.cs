using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvTracker.WebUpload.Models;
using Microsoft.AspNetCore.Mvc;

namespace InvTracker.WebUpload.Controllers
{
    public class AccountController : BaseController
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.UserName == "Admin" && model.Password == "Admin")
                {
                    return RedirectToAction("SalesUpload", "ExcelUpload");
                }
                else
                {
                    ModelState.AddModelError("Invalid", "Please enter valide username or password");
                }
            }
           
            return View();
        }

    }
}