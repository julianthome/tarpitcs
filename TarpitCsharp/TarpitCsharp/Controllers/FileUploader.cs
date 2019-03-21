using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using DefaultNamespace;

namespace TarpitCsharp.Controllers
{
    public class FileUploader : Controller
    {
        private static string _productSourceFolder = Environment.GetEnvironmentVariable("PRODUCT_SRC_FOLDER");
        private static string _productDetinationFolder = Environment.GetEnvironmentVariable("PRODUCT_DST_FOLDER");

        public ActionResult Index(HttpPostedFileBase file)
        {
            
            string fname = file.FileName;
            if (!fname.IsEmpty())
            {
                var path = _productSourceFolder + "/uploads";
                var filename = Path.GetFileName(fname);
                var dest = Path.Combine(path, filename);
                file.SaveAs(dest);
                Unzipper.unzipFile(dest, _productDetinationFolder);
            }

            var res = new JsonResult {Data = "uploaded"};

            return res;
        }
    }
}