using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GangsterBank.Controllers
{
    using System.Diagnostics.Contracts;

    using GangsterBank.DataAccess;
    using GangsterBank.DataAccess.Repositories;
    using GangsterBank.DataAccess.UnitsOfWork;
    using GangsterBank.Domain.Entities;
    using GangsterBank.Domain.Entities.Clients;

    public class HomeController : Controller
    {
        
        public HomeController()
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}