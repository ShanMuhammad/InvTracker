using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvTracker.WebUpload.Models
{
    public class SaleUploadModel
    {
        public int SalesType { get; set; }
        public IFormFile File { get; set; }

    }
}
