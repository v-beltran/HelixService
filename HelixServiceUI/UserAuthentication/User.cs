using HelixService.Utility;
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

        public Guid Guid
        {
            get { return this._guid; }
            set { this._guid = value; }
        }

        public String UserName
        {
            get { return this._user_name; }
            set { this._user_name = value; }
        }

        public String UserPassword
        {
            get { return this._user_password; }
            set { this._user_password = value; }
        }

        public String UserSalt
        {
            get { return this._user_salt; }
            set { this._user_salt = value; }
        }

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
            this.State = ObjectState.Clean;
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
            this.State = ObjectState.Clean;
        }

        #endregion

        #region " Commit Methods "

        /// <summary>
        /// Commit a transaction to the database.
        /// </summary>
        /// <param name="connectionString">The connection string to the database to which transactions will be made.</param>
        public void Commit(String connectionString)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();

                switch (this.State)
                {
                    case ObjectState.Insert:
                        this.InsertObject(cn);
                        break;
                    case ObjectState.Update:
                        this.UpdateObject(cn);
                        break;
                    case ObjectState.Delete:
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
                    cmd.Parameters.AddWithValue("@user_master_guid", this.Guid);
                    cmd.Parameters.AddWithValue("@user_name", this.UserName);
                    cmd.Parameters.AddWithValue("@user_password", this.UserPassword);
                    cmd.Parameters.AddWithValue("@user_salt", this.UserSalt);
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw new Exception("Error: User.InsertObject");
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
                    cmd.Parameters.AddWithValue("@user_master_guid", this.Guid);
                    cmd.Parameters.AddWithValue("@user_name", this.UserName);
                    cmd.Parameters.AddWithValue("@user_password", this.UserPassword);
                    cmd.Parameters.AddWithValue("@user_salt", this.UserSalt);
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw new Exception("Error: User.UpdateObject");
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
                    cmd.Parameters.AddWithValue("@user_master_guid", this.Guid);
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw new Exception("Error: User.DeleteObject");
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
        public static User Load(String connectionString, UserFilter filter)
        {
            List<User> users = User.LoadCollection(connectionString, filter);
            return users.Count > 0 ? users[0] : new User();
        }

        /// <summary>
        /// Load a list of users from the database.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        /// <param name="filter">The filter used to select users.</param>
        /// <returns></returns>
        public static List<User> LoadCollection(String connectionString, UserFilter filter)
        {
            List<User> users = new List<User>();

            SqlCommand cmd = new SqlCommand("SELECT * FROM User_Master");
            String where = String.Empty;

            if (filter.Guid != Guid.Empty)
            {
                where += "user_master_guid=@user_master_guid AND ";
                cmd.Parameters.AddWithValue("@user_master_guid", filter.Guid);
            }

            if (!String.IsNullOrEmpty(filter.UserName))
            {
                where += "user_name=@user_name AND ";
                cmd.Parameters.AddWithValue("@user_name", filter.UserName);
            }

            if (!String.IsNullOrEmpty(filter.UserPassword))
            {
                where += "user_password=@user_password AND ";
                cmd.Parameters.AddWithValue("@user_password", filter.UserPassword);
            }

            if (!String.IsNullOrEmpty(filter.UserSalt))
            {
                where += "user_salt=@user_salt AND ";
                cmd.Parameters.AddWithValue("@user_salt", filter.UserSalt);
            }

            if (!String.IsNullOrEmpty(where))
            {
                cmd.CommandText += " WHERE " + where.Remove(where.Length - 5);
            }

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                using (DataTable dt = HDatabase.GetDataTable(cn, cmd))
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