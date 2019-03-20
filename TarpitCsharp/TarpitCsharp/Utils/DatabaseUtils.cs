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
            
            
            new SQLiteCommand(@"create table order(
              orderId INT,
              custId Int, 
              orderDate varchar(20), 
              orderStatus varchar(20),
              shipDate varchar(20),
              street varchar(20),
              city varchar(20),
              state varchar(20),
              zipCode varchar(20)",
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
                
            new SQLiteCommand(@"INSERT INTO order(orderId, custId, orderDate, orderStatus, shipDate, street, state, zipCode) VALUES (
              1, 
              ""1"", 
              ""2002/01/31"",
              ""completed"",
              ""2002/01/29"",
              ""Downing Street"",
              ""CA"",
              ""3123""",
                _con).ExecuteNonQuery();

            
            
            //_con.Close();
        }
    }
}