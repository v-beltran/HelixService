using HelixService.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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

        public String StateCode
        {
            get { return this._state_ansi_code; }
            set { this._state_ansi_code = value; }
        }

        public String StateName
        {
            get { return this._state_name; }
            set { this._state_name = value; }
        }

        public String StateCapital
        {
            get { return this._state_capital; }
            set { this._state_capital = value; }
        }

        public String StateLargestCity
        {
            get { return this._state_largest_city; }
            set { this._state_largest_city = value; }
        }

        public String StateLargestMetro
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
            this.StateCode = String.Empty;
            this.StateName = String.Empty;
            this.StateCapital = String.Empty;
            this.StateLargestCity = String.Empty;
            this.StateLargestMetro = String.Empty;
        }

        /// <summary>
        /// A state loaded from the database.
        /// </summary>
        /// <param name="dr">The data row to populate the State object.</param>
        public State(DataRow dr)
        {
            this.StateCode = HString.SafeTrim(dr["state_ansi_code"]);
            this.StateName = HString.SafeTrim(dr["state_name"]);
            this.StateCapital = HString.SafeTrim(dr["state_capital"]);
            this.StateLargestCity = HString.SafeTrim(dr["state_largest_city"]);
            this.StateLargestMetro = HString.SafeTrim(dr["state_largest_metro"]);
        }

        #endregion

        #region " Load Methods "

        /// <summary>
        /// Load a single state.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        /// <param name="filter">The filter used to select states.</param>
        /// <returns></returns>
        public static State Load(String connectionString, StateFilter filter)
        {
            List<State> states = State.LoadCollection(connectionString, filter);
            return states.Count > 0 ? states[0] : new State();
        }

        /// <summary>
        /// Load a list of states from the database.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        /// <param name="filter">The filter used to select states.</param>
        /// <returns></returns>
        public static List<State> LoadCollection(String connectionString, StateFilter filter)
        {
            List<State> states = new List<State>();

            SqlCommand cmd = new SqlCommand("SELECT * FROM State_Master");
            String where = String.Empty;

            if (!String.IsNullOrEmpty(filter.StateCode))
            {
                where += "state_ansi_code LIKE '%' + @state_ansi_code + '%' OR ";
                cmd.Parameters.AddWithValue("@state_ansi_code", filter.StateCode);
            }

            if (!String.IsNullOrEmpty(filter.StateName))
            {
                where += "state_name LIKE '%' + @state_name + '%' OR ";
                cmd.Parameters.AddWithValue("@state_name", filter.StateName);
            }

            if (!String.IsNullOrEmpty(filter.StateCapital))
            {
                where += "state_capital LIKE '%' + @state_capital + '%' OR ";
                cmd.Parameters.AddWithValue("@state_capital", filter.StateCapital);
            }

            if (!String.IsNullOrEmpty(filter.StateLargestCity))
            {
                where += "state_largest_city LIKE '%' + @state_largest_city + '%' OR ";
                cmd.Parameters.AddWithValue("@state_largest_city", filter.StateLargestCity);
            }

            if (!String.IsNullOrEmpty(filter.StateLargestMetro))
            {
                where += "state_largest_metro LIKE '%' + @state_largest_metro + '%' OR ";
                cmd.Parameters.AddWithValue("@state_largest_metro", filter.StateLargestMetro);
            }

            if (!String.IsNullOrEmpty(where))
            {
                cmd.CommandText += " WHERE " + where.Remove(where.Length - 4);
            }

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                using (DataTable dt = HDatabase.GetDataTable(cn, cmd))
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
        public String StateCode { get; set; }
        public String StateName { get; set; }
        public String StateCapital { get; set; }
        public String StateLargestCity { get; set; }
        public String StateLargestMetro { get; set; }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public StateFilter()
        {
        }
    }

    #endregion
}