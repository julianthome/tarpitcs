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
//            var httpRequest = HttpContext.Current.Request;
//            string fname = httpRequest.Files["file"];
//            if (httpRequest.Files["file"] != "")
//            {
//                var path = _productSourceFolder + "/uploads";
//                var filename = Path.GetFileName(fname);
//                var dest = Path.Combine(path, filename);
//                httpRequest.Files["file"].SaveAs(dest);
//                Unzipper.unzipFile(dest, _productDetinationFolder);
//            }

            var res = new JsonResult {Data = "uploaded"};

            return res;
        }
    }
}