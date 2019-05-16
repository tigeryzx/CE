using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CE.UpdateServer.Controllers
{
    public class UpdateDownLoadController : Controller
    {
        public FileResult DownLoadUpdaet(string type)
        {
            string UpdateFile = "";
            if (type == "35")
                UpdateFile = "~/UpdatePackage/N35/ceupdatepack.exe";
            else if (type == "40")
                UpdateFile = "~/UpdatePackage/N40/ceupdatepack.exe";

            if (string.IsNullOrEmpty(UpdateFile) || !System.IO.File.Exists(Server.MapPath(UpdateFile)))
            {
                Response.StatusCode = 404;
                Response.End();
            }
            
            string RealFilePath = Server.MapPath(UpdateFile);
            Response.Headers.Add("content-disposition", "filename=ceupdatepack.exe");
            return File(RealFilePath, "application/octet-stream"); ;
        }

    }
}
