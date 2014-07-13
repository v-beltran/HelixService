using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HelixService.Utility
{
    public class HDatabase
    {
        public static DataTable GetDataTable(SqlConnection cn, SqlCommand select)
        {
            DataTable dt = new DataTable();

            using (select.Connection = cn)
            {
                using (SqlDataAdapter adp = new SqlDataAdapter())
                {
                    adp.SelectCommand = select;
                    adp.Fill(dt);
                }
            }

            return dt;
        }
    }
}