using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace TestMVVM
{
    public static class dbClass
    {
        public static SqlConnection GetDBConnection()
        {
            string connection_string = Properties.Settings.Default.connection_string;

            SqlConnection connection = new SqlConnection(connection_string);

            if (connection.State != ConnectionState.Open) connection.Open();

            return connection;
        }

        public static DataTable GetDataTable(string SQL)
        {
            SqlConnection connection = GetDBConnection();

            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter(SQL, connection);

            adapter.Fill(table);

            return table;
        }

        public static void Execute_SQL(string SQL)
        {
            SqlConnection connection = GetDBConnection();

            SqlCommand command = new SqlCommand(SQL, connection);

            command.ExecuteNonQuery();
        }

        public static DataTable Execute_Proc(string procedureName)
        {
            SqlConnection connection = GetDBConnection();

            SqlCommand command = new SqlCommand(procedureName, connection);
            command.CommandType = CommandType.StoredProcedure;
            var dataReader=  command.ExecuteReader();
            var dataTable = new DataTable();
            dataTable.Load(dataReader);
            return dataTable;
        }
        
        public static int InsertTagToDB(string name, bool? IsFellingGoodBad)    
        {
            SqlConnection connection = GetDBConnection();

            SqlCommand command = new SqlCommand("dbo.SaveTag", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@TagName", name));
            command.Parameters.Add(new SqlParameter("@IsFeeling", IsFellingGoodBad));
            return (int) command.ExecuteScalar();
            
        }

        public static void DeleteTagFromDB(int tagID)
        {
            SqlConnection connection = GetDBConnection();

            SqlCommand command = new SqlCommand("dbo.DeleteTag", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@TagID", tagID));

            command.ExecuteNonQuery();
        }

        public static void CloseDBConnection()
        {
            string connection_string = Properties.Settings.Default.connection_string;

            SqlConnection connection = new SqlConnection(connection_string);

            if (connection.State != ConnectionState.Closed) connection.Close();
        }
    }
}
