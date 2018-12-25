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
        private readonly string sqlconnection = ConfigurationManager.ConnectionStrings["DefaultConnString"].ToString();

        public SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            connection.Open();
            return connection;
        }
    }
}
