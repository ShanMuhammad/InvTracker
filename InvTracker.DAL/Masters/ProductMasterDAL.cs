using InvTracker.DAL.Common;
using InvTracker.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvTracker.DAL.Masters
{
    public class ProductMasterDAL
    {
        ADOContext _dbContext = new ADOContext();

        public DataTable SaveProductMaster(ProductMasterModel model)
        {
            DataTable dt = new DataTable();

            SqlParameter[] arrproc;
            string spName = "SaveProductMaster";
            arrproc = new SqlParameter[16];
            arrproc[0] = new SqlParameter("@ProductCategoryId", model.ProductCategoryId);
            arrproc[1] = new SqlParameter("@ProductSubCategoryId", model.ProductSubCategoryId);
            arrproc[2] = new SqlParameter("@ProductCode", model.ProductCode);
            arrproc[3] = new SqlParameter("@ProductName", model.ProductName);
            arrproc[4] = new SqlParameter("@ProductDescription", model.ProductDescription);
            arrproc[5] = new SqlParameter("@ProductCompanyId", model.ProductCompanyId);
            arrproc[6] = new SqlParameter("@Packing", model.Packing);
            arrproc[7] = new SqlParameter("@PurchaseUnitId", model.PurchaseUnitId);
            arrproc[8] = new SqlParameter("@SalesUnitId", model.SalesUnitId);
            arrproc[9] = new SqlParameter("@PuchaseTaxId", model.PuchaseTaxId);
            arrproc[10] = new SqlParameter("@SaleTaxId", model.SaleTaxId);
            arrproc[11] = new SqlParameter("@TaxAplicability", model.TaxAplicability);
            arrproc[12] = new SqlParameter("@HSNCode", model.HSNCode);
            arrproc[13] = new SqlParameter("@SACCode", model.SACCode);
            arrproc[14] = new SqlParameter("@IsActive", model.IsActive);
            arrproc[15] = new SqlParameter("@CreatedBy", model.UserId);
            ADOContext _dbContext = new ADOContext();
            dt = _dbContext.ExecSQLProc(spName, arrproc);
            return dt;
        }

        public DataTable GetProductMaster(ProductMasterModel model)
        {
            DataTable dt = new DataTable();
            SqlParameter[] arrproc;
            string spName = "GetProductMaster";
            ADOContext _dbContext = new ADOContext();
            arrproc = new SqlParameter[7];
            arrproc[0] = new SqlParameter("@PageNumber", model.PageNumber);
            arrproc[1] = new SqlParameter("@RecordSize", model.RecordSize);
            arrproc[2] = new SqlParameter("@ProductCategoryId", model.ProductCategoryId);
            arrproc[3] = new SqlParameter("@ProductSubCategoryId", model.ProductSubCategoryId);
            arrproc[4] = new SqlParameter("@ProductCode", model.ProductCode);
            arrproc[5] = new SqlParameter("@ProductName", model.ProductName);
            arrproc[6] = new SqlParameter("@ProductCompanyId", model.ProductCompanyId);
            dt = _dbContext.ExecSQLProc(spName, arrproc);
            return dt;
        }
    }
}
