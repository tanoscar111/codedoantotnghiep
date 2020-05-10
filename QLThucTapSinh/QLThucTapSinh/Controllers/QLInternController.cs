﻿using PagedList;
using QLThucTapSinh.Common;
using QLThucTapSinh.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using Excel = Microsoft.Office.Interop.Excel;

namespace QLThucTapSinh.Controllers
{
    public class QLInternController : Controller
    {
        // GET: QLIntern
        SQLThucTapSinhEntities database = new SQLThucTapSinhEntities();

        //public ActionResult Index()
        //{
        //    var role = Convert.ToInt32(Session["Role"].ToString());
        //    // School
        //    if(role == 3)
        //    {
        //        var schoolID = Session["SchoolID"].ToString();
        //        var model = (from a in database.Intern
        //                     join b in database.Person on a.PersonID equals b.PersonID
        //                     join d in database.Users on a.PersonID equals d.PersonID
        //                     join e in database.Organization on b.CompanyID equals e.ID
        //                     where b.SchoolID == schoolID && a.InternshipID == null
        //                     select new InternDatabase()
        //                     {
        //                         PersonID = a.PersonID,
        //                         FullName = b.LastName + " " + b.FirstName,
        //                         Birthday = b.Birthday,
        //                         Gender = b.Gender,
        //                         Address = b.Address,
        //                         Phone = b.Phone,
        //                         Email = b.Email,
        //                         Image = b.Image,
        //                         CompanyID = b.CompanyID,
        //                         CompanyName = e.Name,
        //                         InternshipID = a.InternshipID,
        //                         SchoolID = b.SchoolID,
        //                         StudentCode = a.StudentCode,
        //                         Result = a.Result,
        //                         Status = d.Status
        //                     }).OrderByDescending(x => x.Result).ToList();
        //        var count = model.Count();
        //        return View(model);
        //    }
        //    else
        //    {
        //        // Company
        //        if (role == 2)
        //        {
        //            var companyID = Session["CompanyID"].ToString();
        //            var listIn = (from a in database.Person
        //                          join b in database.Intern on a.PersonID equals b.PersonID into joinl1
        //                          from j in joinl1.DefaultIfEmpty()
        //                          join d in database.Users on a.PersonID equals d.PersonID into joinl2
        //                          from k in joinl2.DefaultIfEmpty()
        //                          join f in database.Organization on a.SchoolID equals f.ID into join4
        //                          from p in join4.DefaultIfEmpty()
        //                          where a.CompanyID == companyID && a.RoleID == 5 && j.InternshipID == null
        //                          select new InternDatabase()
        //                          {
        //                              PersonID = a.PersonID,
        //                              FullName = a.LastName + " " + a.FirstName,
        //                              Birthday = a.Birthday,
        //                              Gender = a.Gender,
        //                              Address = a.Address,
        //                              Phone = a.Phone,
        //                              Email = a.Email,
        //                              Image = a.Image,
        //                              CompanyID = a.CompanyID,
        //                              InternshipID = j.InternshipID,
        //                              SchoolID = a.SchoolID,
        //                              SchoolName = p.Name,
        //                              StudentCode = j.StudentCode,
        //                              Result = j.Result,
        //                              Status = k.Status
        //                          }).ToList();
        //            var model = listIn.OrderByDescending(x => x.Result).ToList();
        //            var count = model.Count();
        //            return View(model);
        //        }
        //        //Ledder
        //        else
        //        {
        //            var personID = Session["Person"].ToString();
        //            var model = (from a in database.Intern
        //                        join b in database.InternShip on a.InternshipID equals b.InternshipID
        //                        join c in database.Person on a.PersonID equals c.PersonID
        //                        join d in database.Organization on c.SchoolID equals d.ID
        //                        join e in database.Users on c.PersonID equals e.PersonID
        //                         where b.PersonID == personID
        //                         select new InternDatabase()
        //                         {
        //                             PersonID = a.PersonID,
        //                             FullName = c.LastName + " " + c.FirstName,
        //                             Birthday = c.Birthday,
        //                             Gender = c.Gender,
        //                             Address = c.Address,
        //                             Phone = c.Phone,
        //                             Email = c.Email,
        //                             Image = c.Image,
        //                             CompanyID = c.CompanyID,
        //                             InternshipID = a.InternshipID,
        //                             SchoolID = c.SchoolID,
        //                             SchoolName = d.Name,
        //                             StudentCode = a.StudentCode,
        //                             Result = a.Result,
        //                             Status = e.Status
        //                         }).OrderByDescending(x => x.Result).ToList();
        //            var count = model.Count();
        //            return View(model);
        //        }
        //    }            

        //}

        public ActionResult Index()
        {

            var role = Convert.ToInt32(Session["Role"].ToString());
            // School
            if (role == 3)
            {
                var schoolID = Session["SchoolID"].ToString();
                var model = (from a in database.Intern
                             join b in database.Person on a.PersonID equals b.PersonID
                             join e in database.Organization on b.CompanyID equals e.ID
                             where b.SchoolID == schoolID && a.InternshipID == null
                             select new InternDatabase()
                             {
                                 PersonID = a.PersonID,
                                 FullName = b.LastName + " " + b.FirstName,
                                 Birthday = b.Birthday,
                                 Gender = b.Gender,
                                 Address = b.Address,
                                 Phone = b.Phone,
                                 Email = b.Email,
                                 Image = b.Image,
                                 CompanyID = b.CompanyID,
                                 CompanyName = e.Name,
                                 InternshipID = a.InternshipID,
                                 SchoolID = b.SchoolID,
                                 StudentCode = a.StudentCode,
                                 Result = a.Result,
                             }).OrderByDescending(x => x.CompanyID).ToList();
                var count = model.Count();
                return View(model);
            }
            else
            {
                var model1 = listIShip();
                var companyID = Session["CompanyID"].ToString();
                var listIn = (from a in database.Person
                              join b in database.Intern on a.PersonID equals b.PersonID into joinl1
                              from j in joinl1.DefaultIfEmpty()
                              join f in database.Organization on a.SchoolID equals f.ID into join4
                              from p in join4.DefaultIfEmpty()
                              where a.CompanyID == companyID && a.RoleID == 5 && j.InternshipID == null
                              select new InternDatabase()
                              {
                                  PersonID = a.PersonID,
                                  FullName = a.LastName + " " + a.FirstName,
                                  Birthday = a.Birthday,
                                  Gender = a.Gender,
                                  Address = a.Address,
                                  Phone = a.Phone,
                                  Email = a.Email,
                                  Image = a.Image,
                                  CompanyID = a.CompanyID,
                                  InternshipID = j.InternshipID,
                                  SchoolID = a.SchoolID,
                                  SchoolName = p.Name,
                                  StudentCode = j.StudentCode,
                                  Result = j.Result,
                              }).ToList();
                var model = listIn.OrderByDescending(x => x.SchoolID).ToList();
                var count = model.Count();
                ViewBag.listI = model1;
                return View(model);
            }
        }

        public List<InternShip> listIShip()
        {
            var personID = Session["Person"].ToString();
            var role = Convert.ToInt32(Session["Role"]);
            List<InternShip> model = new List<InternShip>();
            if (role == 4)
            {
                var mo = database.InternShip.Where(x => x.PersonID == personID).ToList();
                foreach (var item in mo)
                {
                    model.Add(item);
                }
            }
            else
            {
                var find = database.Person.Find(personID);
                var list = database.InternShip.Where(x => x.CompanyID == find.CompanyID);
                foreach (var item in list)
                {
                    model.Add(item);
                }
            }
            return model;
        }

        [HttpGet]
        public ActionResult Create()
        {
            var role = Convert.ToInt32(Session["Role"]);

            SetViewBag();
            SetViewBagS();
            SetViewBagG();
            if (role != 3)
            {
                SetViewBagI();
            }
            return View();
        }

        public void SetViewBag(string selectedID = null)
        {
            var list = database.Person.Where(x => x.RoleID == 2).ToList();
            List<Organization> Organ = new List<Organization>();
            foreach (var item in list)
            {
                var model = database.Organization.Find(item.CompanyID);
                Organ.Add(model);
            }
            SelectList OrganList = new SelectList(Organ, "ID", "Name");
            ViewBag.OrganList = OrganList;
        }

        public void SetViewBagS(string selectedID = null)
        {
            var list = database.Person.Where(x => x.RoleID == 3).ToList();
            List<Organization> Organ = new List<Organization>();
            foreach (var item in list)
            {
                var model = database.Organization.Find(item.SchoolID);
                Organ.Add(model);
            }
            SelectList SchoolList = new SelectList(Organ, "ID", "Name");
            ViewBag.SchoolList = SchoolList;
        }

        public void SetViewBagG(string selectedID = null)
        {
            SelectList GenGender = new SelectList(new[] {
                new {Text = "Nam", Value = true},
                new {Text = "Nữ", Value = false},
            }, "Value", "Text");
            ViewBag.GenGender = GenGender;
        }

        public void SetViewBagI(string selectedID = null)
        {
            var role = Convert.ToInt32(Session["Role"]);
            var list = new List<InternShip>();
            if (role == 4)
            {
                var per = Session["Person"].ToString();
                list = database.InternShip.Where(x => x.PersonID == per).ToList();
            }
            else
            {
                var companyID = Session["CompanyID"].ToString();
                list = database.InternShip.Where(x => x.CompanyID == companyID).ToList();
            }
            SelectList IList = new SelectList(list, "InternshipID", "CourseName");
            ViewBag.IList = IList;
        }

        [HttpPost]
        public ActionResult Create(InternDatabase per)
        {
            if (ModelState.IsValid)
            {
                Person person = new Person();
                string personID;
                do
                {
                    personID = new Share().RandomText();
                } while (new CompanyAndSchool().FindPerson(personID) == false);
                person.PersonID = personID;
                database.Person.Add(person);
                person.RoleID = 5;
                person.LastName = per.LastName;
                person.FirstName = per.FirstName;
                person.Birthday = per.Birthday;
                person.Gender = per.Gender;
                person.Address = per.Address;
                person.Phone = per.Phone;
                person.Email = per.Email;

                var role = Convert.ToInt32(Session["Role"].ToString());
                if (role == 3)
                {
                    var schoolID = Session["SchoolID"].ToString();
                    person.SchoolID = schoolID;
                    person.CompanyID = per.CompanyID;
                }
                else
                {
                    var companyID = Session["CompanyID"].ToString();
                    person.SchoolID = per.SchoolID;
                    person.CompanyID = companyID;
                }

                InsertPer(person);
                if (SendMailTK(personID))
                {
                    Intern intern = new Intern();
                    intern.PersonID = personID;
                    intern.StudentCode = per.StudentCode;
                    if(per.InternshipID != null)
                    {
                        intern.InternshipID = per.InternshipID;
                    }
                    intern.Result = 0;
                    InsertInt(intern);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var model = database.Organization.SingleOrDefault(x => x.ID == id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Organization organi)
        {
            if (ModelState.IsValid)
            {

                var model1 = new CompanyAndSchool().Update(organi);
                if (model1)
                {
                    var modle = new Share().listOrgan(2).OrderByDescending(x => x.StartDay).ToList();
                    return RedirectToAction("Index", modle);
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thất bại");
                }
            }
            return View("Edit");
        }

        public ActionResult Delete(string id)
        {
            var model = database.Intern.Find(id);
            database.Intern.Remove(model);
            var model2 = database.Users.SingleOrDefault(x => x.PersonID == id);
            if (model2 != null)
            {
                database.Users.Remove(model2);
            }
            var model1 = database.Person.Find(id);
            database.Person.Remove(model1);
            database.SaveChanges();
            return RedirectToAction("Index", new { id = 0 });
        }

        public ActionResult Delete1(string id, int ins)
        {
            var companyID = Session["CompanyID"].ToString();
            var listIn = (from a in database.Intern
                          join b in database.Person on a.PersonID equals b.PersonID
                          join d in database.Users on a.PersonID equals d.PersonID
                          join e in database.Organization on b.SchoolID equals e.ID
                          where b.CompanyID == companyID
                          select new InternDatabase()
                          {
                              PersonID = a.PersonID,
                              FullName = b.LastName + " " + b.FirstName,
                              Birthday = b.Birthday,
                              Gender = b.Gender,
                              Address = b.Address,
                              Phone = b.Phone,
                              Email = b.Email,
                              Image = b.Image,
                              CompanyID = b.CompanyID,
                              InternshipID = a.InternshipID,
                              SchoolID = b.SchoolID,
                              SchoolName = e.Name,
                              StudentCode = a.StudentCode,
                              Result = a.Result,
                              Status = d.Status
                          }).ToList();
            var role = Convert.ToInt32(Session["Role"].ToString());
            // Company
            if (role == 2)
            {

                var listPer = listIn.OrderByDescending(x => x.Result).ToList();
                return RedirectToAction("Index", listPer);
            }
            //Ledder
            else
            {
                var personID = Session["Person"].ToString();
                var listPer = (from a in listIn
                               join b in database.InternShip on a.InternshipID equals b.InternshipID
                               join c in database.Person on b.PersonID equals c.PersonID
                               where b.PersonID == personID
                               select new InternDatabase()
                               {
                                   PersonID = a.PersonID,
                                   FullName = a.FullName,
                                   Birthday = a.Birthday,
                                   Gender = a.Gender,
                                   Address = a.Address,
                                   Phone = a.Phone,
                                   Email = a.Email,
                                   Image = a.Image,
                                   CompanyID = a.CompanyID,
                                   InternshipID = a.InternshipID,
                                   SchoolID = a.SchoolID,
                                   SchoolName = a.SchoolName,
                                   StudentCode = a.StudentCode,
                                   Result = a.Result,
                                   Status = a.Status
                               }).OrderByDescending(x => x.Result).ToList();
                return RedirectToAction("Index", listPer);
            }
        }
        [HttpGet]
        public ActionResult imPortExcel()
        {
            ViewBag.ListSchool = new Share().listOrgan(2).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult imPortExcel(HttpPostedFileBase excelfile, string organID)
        {
            // Kiểm tra file đó có tồn tại hay không
            if (excelfile == null || excelfile.ContentLength == 0)
            {
                ViewBag.ListSchool = new Share().listOrgan(2).ToList();
                ViewBag.Error = "Thêm File mới<br /> ";
                return View("imPortExcel");
            }
            else
            {
                // kiểm tra đuôi file có phải là file Excel hay không
                if (excelfile.FileName.EndsWith("xls") || excelfile.FileName.EndsWith("xlsx"))
                {
                    // Khai báo đường dẫn
                    string path = Path.Combine("D:/", excelfile.FileName);
                    // Tạo đối tượng COM. Tạo một đối tượng COM cho mọi thứ được tham chiếu
                    Excel.Application application = new Excel.Application();
                    Excel.Workbook workbook = application.Workbooks.Open(path);
                    Excel.Worksheet worksheet = (Excel.Worksheet)workbook.ActiveSheet;
                    Excel.Range range = worksheet.UsedRange;
                    //Lặp lại qua các hàng và cột và in ra bàn điều khiển khi nó xuất hiện trong tệp
                    //excel is not zero based!!
                    var role = Convert.ToInt32(Session["Role"].ToString());
                    if (role == 3)
                    {
                        var schoolID = Session["SchoolID"].ToString();
                        for (int i = 2; i < range.Rows.Count; i++)
                        {
                            Person person = new Person();
                            string personID;
                            do
                            {
                                personID = new Share().RandomText();
                            } while (new CompanyAndSchool().FindPerson(personID) == false);
                            person.PersonID = personID;
                            person.LastName = ((Excel.Range)range.Cells[i, 3]).Text;
                            person.FirstName = ((Excel.Range)range.Cells[i, 4]).Text;
                            DateTime dateValue = DateTime.FromOADate(Convert.ToDouble(((Excel.Range)range.Cells[i, 5]).Value));
                            person.Birthday = dateValue;
                            int gender = int.Parse(((Excel.Range)range.Cells[i, 6]).Text);
                            person.Gender = Convert.ToBoolean(gender);
                            person.Address = ((Excel.Range)range.Cells[i, 7]).Text;
                            person.Phone = ((Excel.Range)range.Cells[i, 8]).Text;
                            person.Email = ((Excel.Range)range.Cells[i, 9]).Text;
                            person.SchoolID = schoolID;
                            person.CompanyID = organID;
                            person.RoleID = 5;
                            //listproducts.Add(product);

                            InsertPer(person);
                            if (SendMailTK(personID))
                            {
                                Intern intern = new Intern();
                                intern.PersonID = personID;
                                intern.StudentCode = ((Excel.Range)range.Cells[i, 2]).Text;
                                intern.Result = 0;
                                InsertInt(intern);
                            }

                        }
                    }
                    else
                    {
                        var companyID = Session["CompanyID"].ToString();
                        for (int i = 2; i < range.Rows.Count; i++)
                        {
                            Person person = new Person();
                            string personID;
                            do
                            {
                                personID = new Share().RandomText();
                            } while (new CompanyAndSchool().FindPerson(personID) == false);
                            person.PersonID = personID;
                            person.LastName = ((Excel.Range)range.Cells[i, 3]).Text;
                            person.FirstName = ((Excel.Range)range.Cells[i, 4]).Text;
                            //person.Birthday = DateTime.ParseExact(((Excel.Range)range.Cells[i, 4]).Text,"yyyy/MM/dd",null);
                            DateTime dateValue = DateTime.FromOADate(Convert.ToDouble(((Excel.Range)range.Cells[i, 5]).Value));
                            person.Birthday = dateValue;
                            int gender = int.Parse(((Excel.Range)range.Cells[i, 6]).Text);
                            //person.Gender = bool.Parse(Convert.ToUInt32(((Excel.Range)range.Cells[i, 5]).Value));
                            person.Gender = Convert.ToBoolean(gender);
                            person.Address = ((Excel.Range)range.Cells[i, 7]).Text;
                            person.Phone = ((Excel.Range)range.Cells[i, 8]).Text;
                            person.Email = ((Excel.Range)range.Cells[i, 9]).Text;
                            person.CompanyID = companyID;
                            person.RoleID = 5;
                            //listproducts.Add(product);
                            InsertPer(person);

                            if (SendMailTK(personID))
                            {
                                Intern intern = new Intern();
                                intern.PersonID = personID;
                                intern.StudentCode = ((Excel.Range)range.Cells[i, 2]).Text;
                                intern.Result = 0;
                                InsertInt(intern);
                            }

                        }
                    }

                    //cleanup
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    //xuất các đối tượng com để dừng hoàn toàn quá trình excel chạy trong nền
                    Marshal.ReleaseComObject(range);
                    Marshal.ReleaseComObject(worksheet);
                    //đóng lại và xuất thông tin
                    workbook.Close();
                    Marshal.ReleaseComObject(workbook);
                    //thoát và xuất thông tin
                    application.Quit();
                    Marshal.ReleaseComObject(application);
                    //ViewBag.ListProduct = listproducts;
                    //dem = listproducts.Count();
                    //return View("List");
                    ViewBag.ListSchool = new Share().listOrgan(2).ToList();
                    ViewBag.Error = "Thêm thành công<br /> ";
                    return View("imPortExcel");
                }
                else
                {
                    ViewBag.ListSchool = new Share().listOrgan(2).ToList();
                    ViewBag.Error = "File không hợp lệ<br /> ";
                    return View("imPortExcel");
                }
            }

        }

        public void InsertPer(Person person)
        {
            database.Person.Add(person);
            database.SaveChanges();
        }

        public void InsertInt(Intern intern)
        {
            database.Intern.Add(intern);
            database.SaveChanges();
        }

        public bool SendMailTK(string personID)
        {
            try
            {
                var res = database.Person.Find(personID);
                string cus = res.LastName + " " + res.FirstName;
                var com = database.Organization.Find(res.CompanyID);
                string compa = com.Name;
                string email = res.Email;
                string nd = personID;
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Email/TaiKhoan1.html"));
                //content = content.Replace("{{CustomerName}}", cus);
                content = content.Replace("{{CompanyName}}", compa);
                content = content.Replace("{{noidung}}", nd);

                var fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
                var fromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
                var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();

                MailMessage message = new MailMessage(fromEmailAddress, email);
                message.Subject = "Cấp mật khẩu mới";
                message.IsBodyHtml = true;
                message.Body = content;

                SmtpClient client = new SmtpClient(smtpHost, 587);
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                NetworkCredential nc = new NetworkCredential(fromEmailAddress, fromEmailPassword);
                client.UseDefaultCredentials = false;
                client.Credentials = nc;
                client.Send(message);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ChangeStatusPer(string id)
        {
            var com = database.Users.SingleOrDefault(x => x.PersonID == id);
            if (com.Status == true)
            {
                com.Status = false;
            }
            else
            {
                com.Status = true;
            }
            database.SaveChanges();
            return com.Status;

        }

        [HttpPost]
        public JsonResult ChangeStatus(string id)
        {
            var res = ChangeStatusPer(id);
            return Json(new
            {
                status = res
            });
        }

    }
}