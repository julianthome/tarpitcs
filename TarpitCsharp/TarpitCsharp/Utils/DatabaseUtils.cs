using System.Data.SQLite;
using System.IO;

namespace TarpitCsharp.Utils
{
    public class DatabaseUtils
    {
        private static string _db = "dummy.db";
        
        public static SQLiteConnection _con;
        
        public static string GetConnectionString()
        {
            return "DataSource=" +_db + ";" ;
        }

        public static void init()
        {
            if(File.Exists(_db))
                File.Delete(_db);
            
            SQLiteConnection.CreateFile(_db);
            
            _con = new SQLiteConnection(GetConnectionString());
            _con.Open();
            new SQLiteCommand(@"create table users(
              id INT, 
              fname varchar(20), 
              lname varchar(20),
              passportnum varchar(20),
              address1 varchar(20),
              address2 varchar(20),
              zipcode varchar(20),
              login varchar(20),
              password varchar(20),
              creditinfo varchar(20)",
              _con).ExecuteNonQuery();
            
            new SQLiteCommand(@"INSERT INTO users(id, fname, lname, passportnum, address1, address2, zipcode, login, password) VALUES (
              1, 
              ""Alice"", 
              ""Test"",
              ""1123"",
              ""Test Avenue"",
              ""CA"",
              ""alice"",
              ""alicepw"",
              ""1323912491293""",
              _con).ExecuteNonQuery();
                

            //_con.Close();
        }
    }
}