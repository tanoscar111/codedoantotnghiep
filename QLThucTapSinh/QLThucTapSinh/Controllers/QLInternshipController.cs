using PagedList;
using QLThucTapSinh.Common;
using QLThucTapSinh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLThucTapSinh.Controllers
{
    public class QLInternshipController : Controller
    {
        // GET: QLInternship
        SQLThucTapSinhEntities database = new SQLThucTapSinhEntities();

        public ActionResult Index(int id)
        {
            var model = listIShip();
            if (id == 0)
            {
                id = model[0].InternshipID;
                Session["InternshipID"] = id;
            }          
            var l = database.IntershipWithTask.Where(x => x.InternshipID == id).OrderBy(x => x.SORT).ToList();
            SelectList chose = new SelectList(l, "SORT", "SORT");
            ViewBag.listID = chose;
            ViewBag.listT = model;
            return View(ListTask(id));
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

        public List<Task> tasks(int id)
        {
            List<Task> tt = new List<Task>();
            var listinandt = database.IntershipWithTask.Where(x => x.InternshipID == id).ToList();
            // danh sách task cú cùng intership
            foreach (var item in listinandt)
            {
                var tas = database.Task.Find(item.TaskID);
                tt.Add(tas);
                // lấy ds task trong bảng task
                // đối tượng task (Find)
            }
            return tt;
        }

        public List<TaskDatabase> ListTask(int id)
        {
            var l = (from a in tasks(id)
                     join b in database.IntershipWithTask on a.TaskID equals b.TaskID
                     where b.InternshipID == id
                     select new TaskDatabase
                     {
                         ID = b.ID,
                         taskid = a.TaskID,
                         taskname = a.TaskName,
                         sort = b.SORT,
                         InternshipID = b.InternshipID
                     }).OrderBy(x => x.sort).ToList();
            return l;
        }

        public ActionResult Dele(int id)
        {
            var xoa = database.IntershipWithTask.Find(id);
            var lsort = database.IntershipWithTask.Where(x => x.InternshipID == xoa.InternshipID && x.SORT > xoa.SORT).ToList();
            foreach (var item in lsort)
            {
                item.SORT = item.SORT - 1;
                database.SaveChanges();
            }
            int count = database.IntershipWithTask.Count();
            var f = database.IntershipWithTask.Find(count);
            xoa.InternshipID = f.InternshipID;
            xoa.TaskID = f.TaskID;
            xoa.SORT = f.SORT;
            database.IntershipWithTask.Remove(f);
            database.SaveChanges();
            Session["InternshipID"] = xoa.InternshipID;
            var tk = Convert.ToInt32(Session["InternshipID"]);
            return RedirectToAction("Index",new { id = tk });
        }

        public ActionResult UpdateSort(int id, int sort)
        {
            var find1 = database.IntershipWithTask.Find(id);
            int sort1 = find1.SORT;
            find1.SORT = sort;
            var find2 = database.IntershipWithTask.SingleOrDefault(x => x.InternshipID == find1.InternshipID && x.SORT == sort);
            find2.SORT = sort1;
            database.SaveChanges();
            Session["InternshipID"] = find1.InternshipID;
            return Content(find1.InternshipID.ToString());
        }

        public bool ChangeStatusInternS(int id)
        {
            var com = database.InternShip.Find(id);
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
        public JsonResult ChangeStatus(int id)
        {
            var res = ChangeStatusInternS(id);
            return Json(new
            {
                status = res
            });
        }

        public ActionResult listT(int tk)
        {
            Session["InternshipID"] = tk;
            var l = database.IntershipWithTask.Where(x => x.InternshipID == tk).OrderBy(x => x.SORT).ToList();
            SelectList chose = new SelectList(l, "SORT", "SORT");
            ViewBag.listID = chose;
            var model = ListTask(tk);
            return PartialView(model);
        }
    }
}