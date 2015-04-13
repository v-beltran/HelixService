using HelixService.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HelixServiceUI.UserAuthentication
{
    public partial class Default : System.Web.UI.Page
    {

        #region " Page Events "

        protected void Page_Init(object sender, EventArgs e)
        {
            // No need to login again if we still have the session.
            if (HttpContext.Current.Session["User_LoggedIn"] != null)
                this.InitLoggedIn(HttpContext.Current.Session["User_LoggedIn"] as User);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #endregion

        /// <summary>
        /// This click event will attempt to log the user in.
        /// </summary>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // Initialize new user.
            User user = new User();

            // Try to find existing user in the database.
            if (this.ValidUser(out user))
            {
                // Show/Hide form controls.
                this.InitLoggedIn(user);

                // Set a session variable to indicate a user is logged in.
                HttpContext.Current.Session["User_LoggedIn"] = user;
            }
            else
            {
                // Doesn't look like the user exists.
                this.lblStatus.Visible = true;
                this.lblStatus.ForeColor = System.Drawing.Color.Red;
                this.lblStatus.Text = "Your username and/or password is incorrect.";
            }
        }

        /// <summary>
        /// Try to find an existing user in the database.
        /// </summary>
        /// <returns>True/False</returns>
        private Boolean ValidUser(out User user)
        {
            Boolean valid = false;
            user = null;

            try
            {
                // Find the user by their username, since this should be unique.
                UserFilter filter = new UserFilter() { UserName = this.txtUsername.Text };
                user = UserAuthentication.User.Load(HConfig.DBConnectionString, filter);

                // Attempt to re-create their hash with the given password and the salt saved in the database.
                String passwordHash = HCryptography.GetHashString(this.txtPassword.Text, user.UserSalt, 256);
                
                if (user != null && user.UserPassword.Equals(passwordHash))
                {
                    // If the hash matches what is in the database, the password is valid.
                    valid = true;
                }

                return valid;
            }
            catch
            {
                    return valid;
            }
        }

        /// <summary>
        /// Show/Hide form controls.
        /// </summary>
        /// <param name="user">The user in session.</param>
        private void InitLoggedIn(User user)
        {
            // Show a welcome message.
            this.lblStatus.Visible = true;
            this.lblStatus.ForeColor = System.Drawing.Color.Green;
            this.lblStatus.Text = String.Format("Welcome {0}!", user.UserName);

            // Hide the login form.
            this.divLogin.Visible = false;

            // Show the logout button.
            this.divLoggedIn.Visible = true;
        }

        /// <summary>
        /// Log the user out.
        /// </summary>
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Terminate user session.
            HttpContext.Current.Session["User_LoggedIn"] = null;

            // Hide login message.
            this.lblStatus.Visible = false;

            // Hide the logout button.
            this.divLoggedIn.Visible = false;

            // Show the login form again.
            this.divLogin.Visible = true;
        }
    }
}