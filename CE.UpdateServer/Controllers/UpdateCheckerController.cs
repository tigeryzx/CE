using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CE.UpdateServer.Controllers
{
    public class UpdateCheckerController : Controller
    {
        /// <summary>
        /// 获取3.5版的更新信息
        /// </summary>
        /// <returns></returns>
        public string GetN35UpdateInfo()
        {
            string Result = System.IO.File.ReadAllText(Server.MapPath("~/UpdatePackage/N35/updateInfo.xml"));
            Response.ContentType = "text/xml";
            return Result;
        }

    }
}
