using InvTracker.WebUpload.Common;
using InvTracker.WebUpload.Models;
using InvTracker.WebUpload.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;


namespace InvTracker.WebUpload.Controllers
{
    public class ExcelUploadController : BaseController
    {
        // private readonly IHostingEnvironment _hostingEnvironment;
        private SalesRepository _SalesRepo;
        private CommonRepository _commonRepo;

        Int64 UserId = 1;
        public ExcelUploadController(IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            //   _hostingEnvironment = hostingEnvironment;
            _SalesRepo = new SalesRepository(configuration);
            _commonRepo = new CommonRepository(configuration);
        }

        public IActionResult SalesUpload(int? id)
        {
            ViewBag.SaleType = id;
            return View();
        }
        [HttpPost]
        public IActionResult SalesUpload(SaleUploadModel model)
        {
            try
            {
                if (model.File == null || model.File.Length == 0)
                {
                    Error("File Not Selected");
                }

                string FileExt = Path.GetExtension(model.File.FileName);
                if (FileExt != ".xlsx")
                {
                    return View();
                }
                var path = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot/ExcelFiles",
                            model.File.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    model.File.CopyTo(stream);
                }
                DataTable dt = GetDataTableFromExcel(path);
                if (dt != null)
                {
                    string Errors = String.Join('\n', ValidateColumnForUpload(dt, model.SalesType));

                    if (string.IsNullOrEmpty(Errors))
                    {
                        bool IsValid = _SalesRepo.ValidateSaleData(UserId, model.SalesType);
                        if (IsValid)
                        {
                            _SalesRepo.InsertSalesData(UserId, model.SalesType);
                            Success("Record Uploaded Successfully!");
                            return View();
                        }
                        else
                        {

                            TempData["SalesData"] = JsonConvert.SerializeObject(_SalesRepo.GetTempUploadSalesData(UserId, model.SalesType));
                            ViewBag.Error = true;
                            Error("Please check Downloaded data and upload again!");
                            // return RedirectToAction("DownloadExcelFile");
                        }
                    }
                    else
                    {
                        Error(Errors);
                    }
                }
                else
                {
                    Error("No Data Found");
                }
            }
            catch (Exception ex)
            {
                string err = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                Error(err);
            }

            ViewBag.SaleType = model.SalesType;
            return View();

        }

        private DataTable GetDataTableFromExcel(string Path, bool HasHeader = true)
        {
            FileInfo fileNew = new FileInfo(Path);
            DataTable dt = new DataTable();
            using (ExcelPackage package = new ExcelPackage(fileNew))
            {
                //ExcelWorksheet workSheet = package.Workbook.Worksheets["Customer"];
                //int totalRows = workSheet.Dimension.Rows;

                ExcelWorksheet ws = package.Workbook.Worksheets.First();

                foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                {
                    dt.Columns.Add(HasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                }
                if (dt.Rows.Count == 0 && dt.Columns.Count == 0)
                {
                    char CName = 'A';
                    for (int i = 0; i < ws.Dimension.End.Column && CName <= 'Z'; i++, CName++)
                    {
                        dt.Columns.Add(CName.ToString());

                    }
                }
                var startRow = HasHeader ? 2 : 1;
                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                    DataRow row = dt.Rows.Add();
                    foreach (var cell in wsRow)
                    {
                        row[cell.Start.Column - 1] = cell.Text;
                    }
                }
            }
            fileNew.Delete();
            return dt;
        }

        private List<string> ValidateColumnForUpload(DataTable dt, int SalesType)
        {
            // Note Sales type 1 for PhoneSale 2 For Recharge Sale
            string[] ColumnList = null;
            List<string> error = new List<string>();

            string ColumnFileName = SalesType == 1 ? "PhoneSaleColumnName.txt" : "RechargeSaleColumnName.txt";


            var columnFile = Path.Combine(
                    Directory.GetCurrentDirectory(), "wwwroot/ExcelFiles",
                    ColumnFileName);
            ColumnList = System.IO.File.ReadAllText(columnFile).Split(',');



            if (ColumnList.Length != dt.Columns.Count)
            {
                error.Add("Please check excel file column count is not same as required column");
                return error;
            }
            bool isValidColumName = true;
            foreach (var item in ColumnList)
            {
                if (!dt.Columns.Contains(item.Replace("\r\n", string.Empty)))
                {
                    isValidColumName = false;
                }
            }
            if (!isValidColumName)
            {
                error.Add("Please check Column in excel");
            }


            if (error.Count == 0)
            {
                _SalesRepo.DeleteTempUploadSalesData(UserId, SalesType);
                if (SalesType == 1) //PhoneSales
                {
                    _SalesRepo.SaveTempPhoneSales(dt, UserId);

                }
                else if (SalesType == 2) //Recharge Sales
                {
                    _SalesRepo.SaveRechargeSales(dt, UserId);
                }
            }

            return error;
        }

        //private static DataTable GetDataTableFromExcel(string path, bool hasHeader = true)
        //{
        //    using (var pck = new OfficeOpenXml.ExcelPackage())
        //    {
        //        using (var stream = File.OpenRead(path))
        //        {
        //            pck.Load(stream);
        //        }
        //        var ws = pck.Workbook.Worksheets.First();
        //        DataTable tbl = new DataTable();
        //        foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
        //        {
        //            tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column { 0}", firstRowCell.Start.Column));
        //        }
        //        var startRow = hasHeader ? 2 : 1;
        //        for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
        //        {
        //            var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
        //            DataRow row = tbl.Rows.Add();
        //            foreach (var cell in wsRow)
        //            {
        //                row[cell.Start.Column - 1] = cell.Text;
        //            }
        //        }
        //        return tbl;
        //    }
        //}


        public IActionResult DownloadExcelFile()
        {

            string export = "SalesData_" + DateTime.Now.ToString();
            DataTable dt = new DataTable();

            //Fill datatable
            dt = JsonConvert.DeserializeObject<DataTable>(TempData["SalesData"].ToString());

            byte[] fileContents;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add(export);
                worksheet.Cells["A1"].LoadFromDataTable(dt, true);
                fileContents = package.GetAsByteArray();
            }
            if (fileContents == null || fileContents.Length == 0)
            {
                return NotFound();
            }
            return File(
                fileContents: fileContents,
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: export + ".xlsx"
            );
        }

        public IActionResult CollectionEntry()
        {
            SalesCollectionVM model = new SalesCollectionVM();
            model.FOSList = _commonRepo.BindDropdown("FOSM").ToSelectList();
            model.RetailerList = _commonRepo.BindDropdown("RETAILER").ToSelectList();
            return View(model);
        }
        [HttpPost]
        public IActionResult CollectionEntry(SalesCollectionVM model)
        {
            try
            {
                _SalesRepo.InsertCollectionEntry(model);
                Success("Record Saved Successfully");
                return RedirectToAction("CollectionEntry");

            }
            catch (Exception)
            {
                Error("Something Went Wrong!!");
            }
            model.FOSList = _commonRepo.BindDropdown("FOSM").ToSelectList();
            model.RetailerList = _commonRepo.BindDropdown("RETAILER").ToSelectList();
            return View(model);
        }
        public IActionResult _PhoneSalesList(string RetailerCode)
        {
            //string FOSCode, 
            List<PhoneSalesVM> model = new List<PhoneSalesVM>();
            model = _SalesRepo.GetSalesDataByRetailerCode(RetailerCode).ToList<PhoneSalesVM>();
            return PartialView(model);
        }

        


    }
}