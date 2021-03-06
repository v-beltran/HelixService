﻿using HelixService.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace HelixServiceUI.UserAuthentication
{
    public class User
    {

        #region " Properties "

        private Guid _guid;
        private String _user_name;
        private String _user_password;
        private String _user_salt;

        /// <summary>
        /// The unique identifier for the user.
        /// </summary>
        public Guid Guid
        {
            get { return this._guid; }
            set
            {
                if (this.State == ObjectState.Unchanged && this._guid != value) { this.State = ObjectState.ToBeUpdated; }
                this._guid = value;
            }
        }

        /// <summary>
        /// The name the user logins with.
        /// </summary>
        public String UserName
        {
            get { return this._user_name; }
            set
            {
                if (this.State == ObjectState.Unchanged && this._user_name != value) { this.State = ObjectState.ToBeUpdated; }
                this._user_name = value;
            }
        }

        /// <summary>
        /// The hashed password used for authentication.
        /// </summary>
        public String UserPassword
        {
            get { return this._user_password; }
            set
            {
                if (this.State == ObjectState.Unchanged && this._user_password != value) { this.State = ObjectState.ToBeUpdated; }
                this._user_password = value;
            }
        }

        /// <summary>
        /// The salt used to further encrypt the password.
        /// </summary>
        public String UserSalt
        {
            get { return this._user_salt; }
            set
            {
                if (this.State == ObjectState.Unchanged && this._user_salt != value) { this.State = ObjectState.ToBeUpdated; }
                this._user_salt = value;
            }
        }

        /// <summary>
        /// The state of the object for SQL-related transactions.
        /// </summary>
        public ObjectState State { get; set; }

        #endregion

        #region " Constructors "

        /// <summary>
        /// A user with empty values.
        /// </summary>
        public User()
        {
            this.Guid = Guid.NewGuid();
            this.UserName = String.Empty;
            this.UserPassword = String.Empty;
            this.UserSalt = String.Empty;
            this.State = ObjectState.ToBeInserted;
        }

        /// <summary>
        /// A user loaded from the database.
        /// </summary>
        /// <param name="dr">The data row to populate the User object.</param>
        public User(DataRow dr)
        {
            this.Guid = Guid.Parse(HString.SafeTrim(dr["user_master_guid"]));
            this.UserName = HString.SafeTrim(dr["user_name"]);
            this.UserPassword = HString.SafeTrim(dr["user_password"]);
            this.UserSalt = HString.SafeTrim(dr["user_salt"]);
            this.State = ObjectState.Unchanged;
        }

        #endregion

        #region " Commit Methods "

        /// <summary>
        /// Commit a transaction to the database.
        /// </summary>        
        public void Commit()
        {
            using (SqlConnection cn = new SqlConnection(HConfig.DBConnectionString))
            {
                cn.Open();

                switch (this.State)
                {
                    case ObjectState.ToBeInserted:
                        this.InsertObject(cn);
                        break;
                    case ObjectState.ToBeUpdated:
                        this.UpdateObject(cn);
                        break;
                    case ObjectState.ToBeDeleted:
                        this.DeleteObject(cn);
                        break;
                    default:
                        break;
                }

                cn.Close();
            }
        }

        /// <summary>
        /// Inserts a new user to the database.
        /// </summary>
        private void InsertObject(SqlConnection cn)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO User_Master (user_master_guid, user_name, user_password, user_salt) VALUES (@user_master_guid, @user_name, @user_password, @user_salt)", cn))
                {
                    cmd.Parameters.Add(new SqlParameter("@user_master_guid", SqlDbType.UniqueIdentifier) { Value = this.Guid });
                    cmd.Parameters.Add(new SqlParameter("@user_name", SqlDbType.NVarChar) { Value = this.UserName });
                    cmd.Parameters.Add(new SqlParameter("@user_password", SqlDbType.NVarChar) { Value = this.UserPassword });
                    cmd.Parameters.Add(new SqlParameter("@user_salt", SqlDbType.NVarChar) { Value = this.UserSalt });
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: User.InsertObject", ex);
            }
        }

        /// <summary>
        /// Updates credentials for the current user.
        /// </summary>
        private void UpdateObject(SqlConnection cn)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE User_Master SET user_master_guid=@user_master_guid, user_name=@user_name, user_password=@user_password, user_salt=@user_salt WHERE user_master_guid=@user_master_guid", cn))
                {
                    cmd.Parameters.Add(new SqlParameter("@user_master_guid", SqlDbType.UniqueIdentifier) { Value = this.Guid });
                    cmd.Parameters.Add(new SqlParameter("@user_name", SqlDbType.NVarChar) { Value = this.UserName });
                    cmd.Parameters.Add(new SqlParameter("@user_password", SqlDbType.NVarChar) { Value = this.UserPassword });
                    cmd.Parameters.Add(new SqlParameter("@user_salt", SqlDbType.NVarChar) { Value = this.UserSalt });
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: User.UpdateObject", ex);
            }
        }

        /// <summary>
        /// Deletes a user by their GUID.
        /// </summary>
        private void DeleteObject(SqlConnection cn)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM User_Master WHERE user_master_guid=@user_master_guid", cn))
                {
                    cmd.Parameters.Add(new SqlParameter("@user_master_guid", SqlDbType.UniqueIdentifier) { Value = this.Guid });
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: User.DeleteObject", ex);
            }
        }

        #endregion

        #region " Load Methods "

        /// <summary>
        /// Load a single user.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        /// <param name="filter">The filter used to select users.</param>
        /// <returns></returns>
        public static User Load(UserFilter filter)
        {
            List<User> users = User.LoadCollection(filter);
            return users.Count > 0 ? users[0] : null;
        }

        /// <summary>
        /// Load a list of users from the database.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        /// <param name="filter">The filter used to select users.</param>
        /// <returns></returns>
        public static List<User> LoadCollection(UserFilter filter)
        {
            List<User> users = new List<User>();
            SqlCommand cmd = new SqlCommand();
            StringBuilder select = new StringBuilder(1000);

            if (filter.Guid != Guid.Empty)
            {
                select.Append("UM.user_master_guid=@user_master_guid");
                cmd.Parameters.Add(new SqlParameter("@user_master_guid", SqlDbType.UniqueIdentifier) { Value = filter.Guid });
            }

            if (!String.IsNullOrEmpty(filter.UserName))
            {
                if (select.Length > 0) { select.Append(" AND "); }
                select.Append("UM.user_name=@user_name");
                cmd.Parameters.Add(new SqlParameter("@user_name", SqlDbType.NVarChar) { Value = filter.UserName });
            }

            if (!String.IsNullOrEmpty(filter.UserPassword))
            {
                if (select.Length > 0) { select.Append(" AND "); }
                select.Append("UM.user_password=@user_password");
                cmd.Parameters.Add(new SqlParameter("@user_password", SqlDbType.NVarChar) { Value = filter.UserPassword });
            }

            if (!String.IsNullOrEmpty(filter.UserSalt))
            {
                if (select.Length > 0) { select.Append(" AND "); }
                select.Append("UM.user_salt=@user_salt");
                cmd.Parameters.Add(new SqlParameter("@user_salt", SqlDbType.NVarChar) { Value = filter.UserSalt });
            }

            select.Insert(0, String.Format("SELECT * FROM User_Master UM {0} ", cmd.Parameters.Count > 0 ? "WHERE" : String.Empty));
            select.Append(" ORDER BY UM.user_name");
            cmd.CommandText = select.ToString();

            using (SqlConnection cn = new SqlConnection(HConfig.DBConnectionString))
            {
                using (DataTable dt = HDatabase.FillDataTable(cn, cmd))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        users.Add(new User(dr));
                    }
                }
            }

            return users;
        }

        #endregion
    }

    #region " User Filter "

    /// <summary>
    /// A filter object to be used for load methods.
    /// </summary>
    public class UserFilter
    {
        public Guid Guid { get; set; }
        public String UserName { get; set; }
        public String UserPassword { get; set; }
        public String UserSalt { get; set; }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public UserFilter()
        {
        }
    }

    #endregion
}