using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvTracker.WebUpload.Models
{
    public class AlertViewModel
    {
        public const string TempDataKey = "TempDataAlerts";
        public string AlertType { get; set; }
        public string AlertTitle { get; set; }
        public string AlertMessage { get; set; }

        //public AlertViewModel(string type, string title, string message)
        //{
        //    AlertType = type;
        //    AlertTitle = title;
        //    AlertMessage = message;
        //}
    }

}
