using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace SQLDataBase
{
    public class Program
    {

        static void Main(string[] args)
        {
            var dataBase = new SQLDataBaseIO("SQLEXPRESS", "TestDataBase");
            //var table = dataBase.ReadAll();
            var data = new List<object> { "Burger", "Cola" };
            //dataBase.Write("TestTable", data);
            //var linkerTable = dataBase.Read("PizdaTable");
            //Console.WriteLine(dataBase.IsTableExists("Goo1ds"));
            SQLTable table = dataBase.LoadTable("Goods");
            table = dataBase.LoadTable("Goods");
            table.Clear();
            table.Append(data);
            var result = table.Read();
            dataBase.DeleteTable("Goods");
            //dataBase.Append("TestCreateTable", data, "First", "Second");
            
            //dataBase.Update("TestCreateTable", "First = Ekimov", data, "First", "Second");
            //dataBase.ClearSheet("PizdaTable");
            
            Console.ReadLine();
        }
    }
}
