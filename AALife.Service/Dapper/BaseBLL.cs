using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AALife.Service.Dapper
{
    public class BaseBLL
    {
        protected readonly string sqlConnection = ConfigurationManager.ConnectionStrings["DefaultConnString"].ToString();

        public SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(sqlConnection);
            connection.Open();
            return connection;
        }
    }
}
