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
    public class QLSchoolController : Controller
    {
        SQLThucTapSinhEntities database = new SQLThucTapSinhEntities();

        // GET: QLSchool
        public ActionResult Index()
        {
            var modle = new Share().listOrgan(3).OrderByDescending(x => x.StartDay).ToList();
            return View(modle);
        }

        
    }
}