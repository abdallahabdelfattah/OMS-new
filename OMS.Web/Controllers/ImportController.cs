using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using System.IO;
using OMS.DAL.DataAccess;
using OMS.BLL.Utilities;

namespace OMS.Web.Controllers
{
    public class ImportController : BaseController
    {
        // GET: Import
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase FileUpload)
        {
            if (FileUpload != null && FileUpload.ContentLength > 0)
            {
                List<string> lstErrors = new List<string>();
                List<string> lstSuccess = new List<string>();

                var UplodeFileName = Path.GetFileName(FileUpload.FileName);

                var path = Path.Combine(Server.MapPath(@"~/App_Data/Uploads"), Path.GetFileName(UplodeFileName));

                string folder = Path.GetDirectoryName(path);
                string date = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");

                string FileType = Path.GetExtension(path);
                if (FileType == ".xlsx" || FileType == ".xls")
                {

                    FileUpload.SaveAs(path);

                    //BusinessException bex = new BusinessException();
                    FileInfo newFile = new FileInfo(path);
                    ExcelPackage pck = new ExcelPackage(newFile);

                    for (int e = 0; e < pck.Workbook.Worksheets.ToList().Count; e++)
                    {
                        ExcelWorksheet workSheet = pck.Workbook.Worksheets.ToList()[e];
                        if (workSheet.Dimension == null) continue;

                        var start = workSheet.Dimension.Start;
                        var end = workSheet.Dimension.End;

                        var endoffeatrues = end.Address.Replace(end.Row.ToString(), "");

                        if (end.Row <= 1)
                        {
                            lstErrors.Add("Import Sheet Cant Be Empty");
                            continue;
                        }

                        if (string.IsNullOrEmpty(workSheet.Cells[1, 2].Text.ToLower()))
                        {
                            lstErrors.Add($" Region column is required ");
                        }

                        if (string.IsNullOrEmpty(workSheet.Cells[1, 3].Text.ToLower()))
                        {
                            lstErrors.Add($" Customer Name column is required ");
                        }

                        if (string.IsNullOrEmpty(workSheet.Cells[1, 4].Text.ToLower()))
                        {
                            lstErrors.Add($" Category column is required ");
                        }

                        if (lstErrors.Count > 0)
                        {
                            break;
                        }


                        Customer customer;

                        for (int i = 2; i <= end.Row; i++)
                        {
                            bool customerObjIsValid = true;
                            customer = new Customer();

                            for (int z = 2; z <= end.Column; z++)
                            {
                                string data = workSheet.Cells[i, z].Text;
                                if (string.IsNullOrEmpty(data))
                                {
                                    lstErrors.Add($" Data in row {i} and column {workSheet.Cells[1, z].Text} is required ");
                                    customerObjIsValid = false;
                                    break;
                                }
                                
                                if (workSheet.Cells[1, z].Text.Trim().ToLower() == "name")
                                {
                                    customer.NameEn = workSheet.Cells[i, z].Text;
                                }
                                
                            }
                            if (customerObjIsValid)
                            {
                                try
                                {
                                    uow.Customers.Add(customer);
                                    lstSuccess.Add($"customer {customer.NameEn} inserted");
                                }
                                catch (BusinessException bexp)
                                {
                                    lstErrors.AddRange(bexp.Exceptions);
                                }
                                catch (Exception ex)
                                {
                                    lstErrors.Add(ex.Message);
                                }
                                
                            }
                        }

                        uow.Save();
                    }

                    //deleting excel file from folder  
                    if ((System.IO.File.Exists(path)))
                    {
                        System.IO.File.Delete(path);
                    }
                }
                else
                {
                    lstErrors.Add("Excel with .xls/.xlsx only allowed");
                }
                ViewBag.Error = lstErrors;
                ViewBag.SaveSuccess = lstSuccess;
            }

            return View("Index");
            //return Json("success", JsonRequestBehavior.AllowGet);
        }

        public FileResult DownloadExcel()
        {
            string path = "/App_Data/Uploads/Template.xlsx";
            return File(path, "application/vnd.ms-excel", "Customers.xlsx");
        }
    }
}