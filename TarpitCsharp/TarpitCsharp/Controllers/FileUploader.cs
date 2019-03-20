using System;
using System.IO;
using System.Web.Mvc;
using DefaultNamespace;

namespace TarpitCsharp.Controllers
{
    public class FileUploader : Controller
    {
        private static string _productSourceFolder = Environment.GetEnvironmentVariable("PRODUCT_SRC_FOLDER");
        private static string _productDetinationFolder = Environment.GetEnvironmentVariable("PRODUCT_DST_FOLDER");

        public ActionResult Index()
        {
            string fname = Request.Files["file"].FileName;
            if (fname != "")
            {
                var path = _productSourceFolder + "/uploads";
                var filename = Path.GetFileName(fname);
                var dest = Path.Combine(path, filename);
                Request.Files["file"].SaveAs(dest);
                Unzipper.unzipFile(dest, _productDetinationFolder);
            }

            var res = new JsonResult {Data = "uploaded"};

            return res;
        }
    }
}