using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvTracker.WebUpload.Models
{
    public class PhoneSalesVM
    {
        public int SalesID { get; set; }
        public string RetailerCode { get; set; }
        public string RetailerName { get; set; }
        public string BrandName { get; set; }
        public decimal Amount { get; set; }
        public decimal ReceivedAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public int BrandType { get; set; }
        public string Model { get; set; }
        public string Material { get; set; }
        public long SKUId { get; set; }
        public string IMEI1 { get; set; }
        public string IMEI2 { get; set; }
        public DateTime ActivationDate { get; set; }
        public int CreatedBy { get; set; }
        public int CreatedByName { get; set; }
        public decimal TotalBalance { get; set; }
        public int SaleType { get; set; }
        public string OrderNo { get; set; }
        public string FOSCode { get; set; }
        public string FOSName { get; set; }
    }
}
