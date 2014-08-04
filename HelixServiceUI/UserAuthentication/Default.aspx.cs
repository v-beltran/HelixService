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
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// This click event will attempt to log the user in.
        /// </summary>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // An error or success message will be displayed.
            this.lblStatus.Visible = true;

            // Try to find existing user in the database.
            if (this.ValidUser())
            {
                // Show a welcome message.
                this.lblStatus.ForeColor = System.Drawing.Color.Green;
                this.lblStatus.Text = String.Format("Welcome {0}!", this.txtUsername.Text);

                // Hide the login form.
                this.divLogin.Visible = false;
                
                // Show the logout button.
                this.divLoggedIn.Visible = true;
            }
            else
            {
                // Doesn't look like the user exists.
                this.lblStatus.ForeColor = System.Drawing.Color.Red;
                this.lblStatus.Text = "Your username and/or password is incorrect.";
            }
        }

        private Boolean ValidUser()
        {
            try
            {
                // Find the user by their username, since this should be unique.
                UserFilter filter = new UserFilter() { UserName = this.txtUsername.Text };
                User user = UserAuthentication.User.Load(WebConfigurationManager.AppSettings["ConnString"], filter);

                // Attempt to re-create their hash with the given password and the salt saved in the database.
                String passwordHash = HCryptography.GetHashString(this.txtPassword.Text, user.UserSalt, 256);

                // If the hash matches what is in the database, the password is valid.
                if (user.UserPassword.Equals(passwordHash))
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Log the user out.
        /// </summary>
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Hide login message.
            this.lblStatus.Visible = false;

            // Hide the logout button.
            this.divLoggedIn.Visible = false;

            // Show the login form again.
            this.divLogin.Visible = true;
        }
    }
}