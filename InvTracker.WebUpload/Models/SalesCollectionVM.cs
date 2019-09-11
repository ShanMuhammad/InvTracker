using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InvTracker.WebUpload.Models
{
    public class SalesCollectionVM
    {
        public SalesCollectionVM()
        {
            SelectedSalesIds = new List<int>();
        }
        [Required(ErrorMessage = "Please Select FOS")]
        public string FOSCode { get; set; }
        public SelectList FOSList { get; set; }
        [Required(ErrorMessage = "Please Select Retailer")]
        public string RetailerCode { get; set; }
        public SelectList RetailerList { get; set; }
        public decimal? TotalBalance { get; set; }
        [Required]
        public decimal? ReceivedAmount { get; set; }
        public decimal? SelectedBalance { get; set; }
        [Required(ErrorMessage = "Please Enter Receipt Number")]
        public string ReceiptNumber { get; set; }
        [Required(ErrorMessage = "Please Select Receipt Date")]
        public DateTime? ReceiptDate { get; set; }
        public List<int> SelectedSalesIds { get; set; }
    }
}
