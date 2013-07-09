using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Helpers;

namespace WebSite.Controllers
{
    [SessionAuthorize]
    public class RolesController : Controller
    {
        //
        // GET: /Roles/

        public ActionResult Roles()
        {
            return View();
        }

    }
}
