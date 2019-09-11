using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvTracker.Model
{
    public class ProductMasterModel : BaseModel
    {
        public Int64 ProductId { get; set; }
        public int? ProductCategoryId { get; set; }
        public int? ProductSubCategoryId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int? ProductCompanyId { get; set; }
        public string Packing { get; set; }
        public int? PurchaseUnitId { get; set; }
        public int? SalesUnitId { get; set; }
        public int? PuchaseTaxId { get; set; }
        public int? SaleTaxId { get; set; }
        public int? TaxAplicability { get; set; }
        public string HSNCode { get; set; }
        public string SACCode { get; set; }
        public bool IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ProductCategoryName { get; set; }
        public string ProductSubCategoryName { get; set; }
        public string ProductCompanyName { get; set; }
    }
}
