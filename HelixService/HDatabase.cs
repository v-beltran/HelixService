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
        /// <summary>
        /// Gets a DataTable object from records selected from a database table.
        /// </summary>
        /// <param name="cn">The connection string to the database.</param>
        /// <param name="select">The command to select from the database.</param>
        /// <returns></returns>
        public static DataTable GetDataTable(SqlConnection cn, SqlCommand select)
        {
            DataTable dt = new DataTable();

            using (select.Connection = cn)
            {
                cn.Open();

                using (SqlDataAdapter adp = new SqlDataAdapter())
                {
                    adp.SelectCommand = select;
                    adp.Fill(dt);
                }

                cn.Close();
            }

            return dt;
        }
    }

    /// <summary>
    /// Named states to easily indicate what type
    /// of action will be committed to an object.
    /// </summary>
    public enum ObjectState
    {
        Insert = 0,
        Update = 1,
        Delete = 2
    }
}