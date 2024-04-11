using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace delegate_main
{
    public class dbhelper
    {
        private SqlConnection _cnn;
        public dbhelper(String s)
        {
            _cnn = new SqlConnection(s);
        }
        public DataTable getRecords(String query, SqlParameter[] parameters = null)
        {
            SqlDataAdapter da = new SqlDataAdapter(query, _cnn);
            DataTable data = new DataTable();
            if (parameters != null)
            {
                da.SelectCommand.Parameters.AddRange(parameters);
            }
            _cnn.Open();
            da.Fill(data);
            _cnn.Close();
            return data;
        }
        public void ExucteDB(String query, SqlParameter[] parameters = null)
        {
            SqlCommand command = new SqlCommand(query, _cnn);
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            _cnn.Open();
            command.ExecuteNonQuery();
            _cnn.Close();
        }
    }
}