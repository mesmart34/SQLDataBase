using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SQLDataBase
{
    public class SQLTableIO
    {
        private SqlConnection _connection;

        public SQLTableIO(string datasource, string database, string username, string password)
        {
            string arguments = @"Data Source=" + datasource 
                + ";Initial Catalog=" + database + 
                ";Persist Security Info=True;User ID=" + username + 
                ";Password=" + password;
            _connection = new SqlConnection(arguments);
        }

        public List<IList<string>> ReadAll()
        {
            var result = new List<IList<string>>();
            
            return result;
        }
    }
}
