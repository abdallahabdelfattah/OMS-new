﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OMS.Web.Controllers
{
    public class AccessDeniedController : BaseController
    {
        // GET: AccessDenied
        public ActionResult Index()
        {
            return View();
        }
    }
}