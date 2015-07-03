using HelixService.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace HelixServiceUI.SearchAJAX
{
    public class State
    {
        #region " Properties "

        private String _state_ansi_code;
        private String _state_name;
        private String _state_capital;
        private String _state_largest_city;
        private String _state_largest_metro;

        public String AnsiCode
        {
            get { return this._state_ansi_code; }
            set { this._state_ansi_code = value; }
        }

        public String Name
        {
            get { return this._state_name; }
            set { this._state_name = value; }
        }

        public String Capital
        {
            get { return this._state_capital; }
            set { this._state_capital = value; }
        }

        public String LargestCity
        {
            get { return this._state_largest_city; }
            set { this._state_largest_city = value; }
        }

        public String LargestMetro
        {
            get { return this._state_largest_metro; }
            set { this._state_largest_metro = value; }
        }

        #endregion

        #region " Constructors "

        /// <summary>
        /// A state with empty values.
        /// </summary>
        public State()
        {
            this.AnsiCode = String.Empty;
            this.Name = String.Empty;
            this.Capital = String.Empty;
            this.LargestCity = String.Empty;
            this.LargestMetro = String.Empty;
        }

        /// <summary>
        /// A state loaded from the database.
        /// </summary>
        /// <param name="dr">The data row to populate the State object.</param>
        public State(DataRow dr)
        {
            this.AnsiCode = HString.SafeTrim(dr["state_ansi_code"]);
            this.Name = HString.SafeTrim(dr["state_name"]);
            this.Capital = HString.SafeTrim(dr["state_capital"]);
            this.LargestCity = HString.SafeTrim(dr["state_largest_city"]);
            this.LargestMetro = HString.SafeTrim(dr["state_largest_metro"]);
        }

        #endregion

        #region " Load Methods "

        /// <summary>
        /// Load a single state.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        /// <param name="filter">The filter used to select states.</param>
        /// <returns></returns>
        public static State Load(StateFilter filter)
        {
            List<State> states = State.LoadCollection(filter);
            return states.Count > 0 ? states[0] : null;
        }

        /// <summary>
        /// Load a list of states from the database.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        /// <param name="filter">The filter used to select states.</param>
        /// <returns></returns>
        public static List<State> LoadCollection(StateFilter filter)
        {
            List<State> states = new List<State>();
            SqlCommand cmd = new SqlCommand();
            StringBuilder select = new StringBuilder(1000);

            if (!String.IsNullOrEmpty(filter.Code))
            {
                select.Append("SM.state_ansi_code LIKE '%' + @state_ansi_code + '%'");
                cmd.Parameters.Add(new SqlParameter("@state_ansi_code", SqlDbType.NVarChar) { Value = filter.Code });
            }

            if (!String.IsNullOrEmpty(filter.Name))
            {
                if (select.Length > 0) { select.Append(" OR "); }
                select.Append("SM.state_name LIKE '%' + @state_name + '%'");
                cmd.Parameters.Add(new SqlParameter("@state_name", SqlDbType.NVarChar) { Value = filter.Name });
            }

            if (!String.IsNullOrEmpty(filter.Capital))
            {
                if (select.Length > 0) { select.Append(" OR "); }
                select.Append("SM.state_capital LIKE '%' + @state_capital + '%'");
                cmd.Parameters.Add(new SqlParameter("@state_capital", SqlDbType.NVarChar) { Value = filter.Capital });
            }

            if (!String.IsNullOrEmpty(filter.LargestCity))
            {
                if (select.Length > 0) { select.Append(" OR "); }
                select.Append("SM.state_largest_city LIKE '%' + @state_largest_city + '%'");
                cmd.Parameters.Add(new SqlParameter("@state_largest_city", SqlDbType.NVarChar) { Value = filter.LargestCity });
            }

            if (!String.IsNullOrEmpty(filter.LargestMetro))
            {
                if (select.Length > 0) { select.Append(" OR "); }
                select.Append("SM.state_largest_metro LIKE '%' + @state_largest_metro + '%'");
                cmd.Parameters.Add(new SqlParameter("@state_largest_metro", SqlDbType.NVarChar) { Value = filter.LargestMetro });
            }

            select.Insert(0, String.Format("SELECT * FROM State_Master SM {0} ", cmd.Parameters.Count > 0 ? "WHERE" : String.Empty));
            select.Append(" ORDER BY SM.state_name");
            cmd.CommandText = select.ToString();

            using (SqlConnection cn = new SqlConnection(HConfig.DBConnectionString))
            {
                using (DataTable dt = HDatabase.FillDataTable(cn, cmd))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        states.Add(new State(dr));
                    }
                }
            }

            return states;
        }

        #endregion
    }

    #region " State Filter "

    /// <summary>
    /// A filter object to be used for load methods.
    /// </summary>
    public class StateFilter
    {
        public String Code { get; set; }
        public String Name { get; set; }
        public String Capital { get; set; }
        public String LargestCity { get; set; }
        public String LargestMetro { get; set; }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public StateFilter()
        {
        }
    }

    #endregion
}