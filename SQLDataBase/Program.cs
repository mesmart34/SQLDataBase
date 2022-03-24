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
            var data = new List<object> { 3, "Artyom", "Red" };
            //dataBase.Write("TestTable", data);
            //var linkerTable = dataBase.Read("PizdaTable");
            /*if (dataBase.IsTableExists("Students"))
            {
                dataBase.DeleteTable("Students");
            }
*//*if (dataBase.IsTableExists("Students"))
            {
                dataBase.DeleteTable("Students");
            }
*/
            /*SQLTable table = dataBase.CreateTable("Students", "Id int", "Name text", "State text");*/
            SQLTable table = dataBase.LoadTable("Students");
            table.Update("Id = 1", data, "Id", "Name", "State");
            var result = table.Read();
            //dataBase.Append("TestCreateTable", data, "First", "Second");

            //dataBase.Update("TestCreateTable", "First = Ekimov", data, "First", "Second");
            //dataBase.ClearSheet("PizdaTable");

            Console.ReadLine();
        }
    }
}
