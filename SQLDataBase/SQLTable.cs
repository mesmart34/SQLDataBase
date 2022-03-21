using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SQLDataBase
{
    public class SQLTable
    {
        public string[] ColumnsName { get; private set; }
        public string Name { get; private set; }
        private SqlConnection _connection;

        public static SQLTable CreateSQLTable(SqlConnection connection, string name, params string[] columns)
        {
            var query = @"CREATE TABLE " + name + " (" + string.Join(", ", columns) + ");";
            var command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            var table = new SQLTable();
            table._connection = connection;
            table.Name = name;
            table.ColumnsName = columns;
            return table;
        }

        public static SQLTable LoadSQLTable(SqlConnection connection, string name)
        {
            var restrictions = new string[4] { null, null, name, null };
            var columnList = connection.GetSchema("Columns", restrictions).AsEnumerable().Select(s => s.Field<String>("Column_Name")).ToList();
            var table = new SQLTable();
            var colList = new List<string>();
            var dataTable = new DataTable();
            var cmdString = string.Format("SELECT TOP 0 * FROM {0}", name);
            using (SqlDataAdapter dataContent = new SqlDataAdapter(cmdString, connection))
            {
                dataContent.Fill(dataTable);
                foreach (DataColumn col in dataTable.Columns)
                {
                    colList.Add(col.ColumnName);
                }
            }
            table._connection = connection;
            table.Name = name;
            table.ColumnsName = colList.ToArray();
            return table;
        }


        public IList<IList<object>> Read(string condition = "")
        {
            var result = new List<IList<object>>();
            var query = @"SELECT * FROM " + Name + (condition.Length > 0 ? " WHERE " + condition : "");
            var command = new SqlCommand(query, _connection);
            var reader = command.ExecuteReader();
            if (!reader.HasRows)
                return null;
            while (reader.Read())
            {
                var row = new List<object>();
                var columns = reader.FieldCount;
                for (var column = 0; column < columns; column++)
                {
                    var value = reader.GetValue(column);
                    row.Add(value);
                }
                result.Add(row);
            }
            reader.Close();
            return result;
        }

        public void Append(List<object> data)
        {
            var query = @"INSERT INTO " + Name + " VALUES(@" + string.Join(", @", ColumnsName) + ");";
            var command = new SqlCommand(query, _connection);
            for (var i = 0; i < ColumnsName.Length; i++)
            {
                var rowName = "@" + ColumnsName[i];
                command.Parameters.AddWithValue(rowName, data[i]);
            }
            command.ExecuteNonQuery();
        }

        public void Update(string condition, List<object> data, params string[] rowNames)
        {
            var query = @"UPDATE " + Name + " SET(@" + string.Join(", @", rowNames) + ") WHERE " + condition + ";";
            var command = new SqlCommand(query, _connection);
            for (var i = 0; i < data.Count; i++)
            {
                var rowName = "@" + rowNames[i];
                command.Parameters.AddWithValue(rowName, data[i]);
            }
            int rows = command.ExecuteNonQuery();
        }


        public void Clear()
        {
            var query = @"TRUNCATE TABLE " + Name + ";";
            var command = new SqlCommand(query, _connection);
            command.ExecuteNonQuery();
        }
    }
}
