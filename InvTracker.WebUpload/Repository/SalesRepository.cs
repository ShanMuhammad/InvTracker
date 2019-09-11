using InvTracker.WebUpload.Common;
using InvTracker.WebUpload.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace InvTracker.WebUpload.Repository
{

    public class SalesRepository
    {
        ADOContext _dbContext;
        string ConnectionString;
        public SalesRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration.GetConnectionString("InvTrackerContext");
            _dbContext = new ADOContext(ConnectionString);

        }
        public IConfiguration Configuration { get; }

        public void SaveTempPhoneSales(DataTable dt, Int64 UserId)
        {

            SqlBulkCopy objbulk = new SqlBulkCopy(ConnectionString);
            try
            {
                dt.Columns.Add("CreatedBy", typeof(System.Int64));
                foreach (DataRow dr in dt.Rows)
                {
                    dr["CreatedBy"] = UserId;
                }

                objbulk.DestinationTableName = "TempPhoneSales";

                objbulk.ColumnMappings.Add("Fos Code", "FOSCode");
                objbulk.ColumnMappings.Add("Retailer Code", "RetailerCode");
                objbulk.ColumnMappings.Add("Retailer Name", "RetailerName");
                objbulk.ColumnMappings.Add("Brand Name", "BrandName");
                objbulk.ColumnMappings.Add("Amount", "Amount");
                objbulk.ColumnMappings.Add("Brand Type", "BrandType");
                objbulk.ColumnMappings.Add("Model", "Model");
                objbulk.ColumnMappings.Add("Material", "Material");
                objbulk.ColumnMappings.Add("SKU ID", "SKUId");
                objbulk.ColumnMappings.Add("IMEI 1", "IMEI1");
                objbulk.ColumnMappings.Add("Activation Date", "ActivationDate");
                objbulk.ColumnMappings.Add("CreatedBy", "CreatedBy");
                objbulk.WriteToServer(dt);
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("Received an invalid column length from the bcp client for colid"))
                {
                    string pattern = @"\d+";
                    Match match = Regex.Match(ex.Message.ToString(), pattern);
                    var index = Convert.ToInt32(match.Value) - 1;

                    FieldInfo fi = typeof(SqlBulkCopy).GetField("_sortedColumnMappings", BindingFlags.NonPublic | BindingFlags.Instance);
                    var sortedColumns = fi.GetValue(objbulk);
                    var items = (Object[])sortedColumns.GetType().GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(sortedColumns);

                    FieldInfo itemdata = items[index].GetType().GetField("_metadata", BindingFlags.NonPublic | BindingFlags.Instance);
                    var metadata = itemdata.GetValue(items[index]);

                    var column = metadata.GetType().GetField("column", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).GetValue(metadata);
                    var length = metadata.GetType().GetField("length", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).GetValue(metadata);
                    // throw new DataFormatException(String.Format("Column: {0} contains data with a length greater than: {1}", column, length));
                }

                throw;
            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally { }
        }
        public void SaveRechargeSales(DataTable dt, Int64 UserId)
        {

            SqlBulkCopy objbulk = new SqlBulkCopy(ConnectionString, SqlBulkCopyOptions.FireTriggers);
            try
            {
                dt.Columns.Add("CreatedBy", typeof(System.Int64));
                foreach (DataRow dr in dt.Rows)
                {
                    dr["CreatedBy"] = UserId;
                }

                objbulk.DestinationTableName = "TempRechargeSales";

                objbulk.ColumnMappings.Add("Fos Order No", "OrderNo");
                objbulk.ColumnMappings.Add("Fos Code", "FosCode");
                objbulk.ColumnMappings.Add("Fos Name", "FosName");
                objbulk.ColumnMappings.Add("Sale Date", "SaleDate");
                objbulk.ColumnMappings.Add("Retailer Code", "RetailerCode");
                objbulk.ColumnMappings.Add("Retailer Name", "RetailerName");
                objbulk.ColumnMappings.Add("Article Category", "ArticleCategory");
                objbulk.ColumnMappings.Add("Article Desc", "ArticleDesc");
                objbulk.ColumnMappings.Add("Article No", "ArticleNo");
                objbulk.ColumnMappings.Add("Unit", "Unit");
                objbulk.ColumnMappings.Add("CreatedBy", "CreatedBy");
                objbulk.WriteToServer(dt);
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("Received an invalid column length from the bcp client for colid"))
                {
                    string pattern = @"\d+";
                    Match match = Regex.Match(ex.Message.ToString(), pattern);
                    var index = Convert.ToInt32(match.Value) - 1;

                    FieldInfo fi = typeof(SqlBulkCopy).GetField("_sortedColumnMappings", BindingFlags.NonPublic | BindingFlags.Instance);
                    var sortedColumns = fi.GetValue(objbulk);
                    var items = (Object[])sortedColumns.GetType().GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(sortedColumns);

                    FieldInfo itemdata = items[index].GetType().GetField("_metadata", BindingFlags.NonPublic | BindingFlags.Instance);
                    var metadata = itemdata.GetValue(items[index]);

                    var column = metadata.GetType().GetField("column", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).GetValue(metadata);
                    var length = metadata.GetType().GetField("length", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).GetValue(metadata);
                    // throw new DataFormatException(String.Format("Column: {0} contains data with a length greater than: {1}", column, length));
                }

                throw;
            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally { }
        }

        public bool ValidateSaleData(Int64 UserId, int SaleType)
        {
            SqlParameter[] arrproc;
            bool IsValid = false;
            string spName = "ValidateSaleData";
            arrproc = new SqlParameter[2];
            arrproc[0] = new SqlParameter("@UserId", UserId);
            arrproc[1] = new SqlParameter("@SaleType", SaleType);
            DataTable dt = _dbContext.ExecSQLProc(spName, arrproc);
            if (dt != null && dt.Rows.Count > 0)
            {
                IsValid = Convert.ToBoolean(dt.Rows[0]["IsValid"]);
            }
            return IsValid;
        }

        public bool InsertSalesData(Int64 UserId, int SaleType)
        {
            SqlParameter[] arrproc;
            bool IsValid = false;
            string spName = "InsertSalesData";
            arrproc = new SqlParameter[2];
            arrproc[0] = new SqlParameter("@UserId", UserId);
            arrproc[1] = new SqlParameter("@SaleType", SaleType);
            DataTable dt = _dbContext.ExecSQLProc(spName, arrproc);
            if (dt != null && dt.Rows.Count > 0)
            {
                IsValid = Convert.ToBoolean(dt.Rows[0]["IsValid"]);
            }
            return IsValid;
        }

        public void DeleteTempUploadSalesData(Int64 UserId, int SalesType)
        {
            SqlParameter[] arrproc;
            string spName = "DeleteTempUploadSalesData";
            arrproc = new SqlParameter[2];
            arrproc[0] = new SqlParameter("@UserId", UserId);
            arrproc[1] = new SqlParameter("@SalesType", SalesType);
            _dbContext.ExecSQLProc(spName, arrproc);
        }

        public DataTable GetTempUploadSalesData(Int64 UserId, int SalesType)
        {
            SqlParameter[] arrproc;
            string spName = "GetTempUploadSalesData";
            arrproc = new SqlParameter[2];
            arrproc[0] = new SqlParameter("@UserId", UserId);
            arrproc[1] = new SqlParameter("@SalesType", SalesType);
            return _dbContext.ExecSQLProc(spName, arrproc);
        }

        public DataTable GetSalesDataByRetailerCode(string RetailerCode)
        {
            SqlParameter[] arrproc;
            string spName = "GetSalesDataByRetailerCode";
            arrproc = new SqlParameter[1];
            arrproc[0] = new SqlParameter("@RetailerCode", RetailerCode);
            return _dbContext.ExecSQLProc(spName, arrproc);
        }


        public DataTable InsertCollectionEntry(SalesCollectionVM model)
        {
            string strSelectedSalesIds = String.Join(',', model.SelectedSalesIds);
            SqlParameter[] arrproc;
            string spName = "InsertCollectionEntry";
            arrproc = new SqlParameter[6];
            arrproc[0] = new SqlParameter("@FOSCode", model.FOSCode);
            arrproc[1] = new SqlParameter("@RetailerCode", model.RetailerCode);
            arrproc[2] = new SqlParameter("@ReceivedAmount", model.ReceivedAmount);
            arrproc[3] = new SqlParameter("@ReceiptNumber", model.ReceiptNumber);
            arrproc[4] = new SqlParameter("@ReceiptDate", model.ReceiptDate);
            arrproc[5] = new SqlParameter("@SelectedSalesIds", strSelectedSalesIds);
            return _dbContext.ExecSQLProc(spName, arrproc);
        }

    }
}
