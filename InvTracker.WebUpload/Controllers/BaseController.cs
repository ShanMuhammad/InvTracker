using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvTracker.WebUpload.Common;
using InvTracker.WebUpload.Models;
using Microsoft.AspNetCore.Mvc;


namespace InvTracker.WebUpload.Controllers
{
    public class BaseController : Controller
    {
       
        public void Success(string Message)
        {
           // ViewBag.Messages = new AlertViewModel("success", "Success!", Message);
            AddAlert("success", "Success!", Message);
        }
        public void Warning(string Message)
        {
            AddAlert("warning", "Warning!", Message);
            //ViewBag.Messages = new AlertViewModel("warning", "Warning!", Message);
        }
        public void Error(string Message)
        {
            AddAlert("danger", "Danger!", Message);
           // ViewBag.Messages = new AlertViewModel("danger", "Danger!", Message);
        }
        //ViewBag.Messages = new AlertViewModel("danger", "Danger!", "The object was not added!");
        //  new[] {
        //new AlertViewModel("success", "Success!", "The object was added successfully!"),
        //new AlertViewModel("warning", "Warning!", "The object was added with a warning!"),
        //new AlertViewModel("danger", "Danger!", "The object was not added!")
        //};

        private void AddAlert(string alertStyle, string header, string message)
        {

            TempData.Put(AlertViewModel.TempDataKey, new AlertViewModel
            {
                AlertType = alertStyle,
                AlertMessage = message,
                AlertTitle = header
            });
        }
    }
}