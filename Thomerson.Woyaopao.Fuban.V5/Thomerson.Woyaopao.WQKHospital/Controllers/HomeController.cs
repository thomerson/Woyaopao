using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Thomerson.Woyaopao.WQKHospital.Controllers
{
    public class HomeController : Controller
    {

        private static List<string> teamList = new List<string>() {
            "管理一部门工会",
            "管理二部门工会",
            "管理三部门工会",
            "医技一部门工会",
            "医技二部门工会",
            "医技三部门工会",
            "胸外科部门工会",
            "手术麻醉部门工会",
            "呼吸内科部门工会",
            "心内科部门工会",
            "心外科部门工会",
            "医学中心部门工会"
        };

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


    }
}