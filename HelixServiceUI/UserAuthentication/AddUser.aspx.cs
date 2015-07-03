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
    public partial class AddUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Adds a new user to the database during click event.
        /// </summary>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            // An error or success message will be displayed.
            this.lblStatus.Visible = true;

            // Require a minimum password length.
            if (this.txtPassword.Text.Length >= 8)
            {
                // Commit user to database when password input fields match.
                if (this.txtConfirmPassword.Text.Equals(this.txtPassword.Text))
                {
                    // Check if the username is taken.
                    if (this.ValidUsername())
                    {
                        // Try to insert new user to the database.
                        this.InsertUser();
                    }
                    else
                    {
                        // Display error when username exists already.
                        this.lblStatus.ForeColor = System.Drawing.Color.Red;
                        this.lblStatus.Text = String.Format("Error: {0} is already taken.", this.txtUsername.Text);
                    }
                }
                else
                {
                    // Display error when input fields are not correct.
                    this.lblStatus.ForeColor = System.Drawing.Color.Red;
                    this.lblStatus.Text = "Error: The specified passwords do not match.";
                }
            }
            else
            {
                // Display error when password is not long enough.
                this.lblStatus.ForeColor = System.Drawing.Color.Red;
                this.lblStatus.Text = "Error: Your password must be at least 8 characters.";
            }
        }

        /// <summary>
        /// Check if the username is already taken.
        /// </summary>
        /// <returns></returns>
        private Boolean ValidUsername()
        {
            Boolean valid = false;

            try
            {
                // Filter users by username.
                UserFilter filter = new UserFilter() { UserName = this.txtUsername.Text };

                // Attempt to load a list of users based on the above filter.
                List<User> users = UserAuthentication.User.LoadCollection(filter);

                // A valid username is one that does not exist in the database yet.
                if (users.Count == 0)
                {
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
        /// This function will attempt to add a user to the database 
        /// from a given username and password on the form.
        /// </summary>
        private void InsertUser()
        {
            try
            {
                // Initialize new User object.
                User user = new UserAuthentication.User();
                user.UserName = this.txtUsername.Text;
                user.UserSalt = HCryptography.BytesToHexString(HCryptography.GetRandomSalt(256));
                user.UserPassword = HCryptography.GetHashString(this.txtPassword.Text, user.UserSalt, 256);
                user.Commit();

                // Display success message.
                this.lblStatus.ForeColor = System.Drawing.Color.Green;
                this.lblStatus.Text = "Success: This user has been added to the database.";

                // Clear form for postback.
                this.ClearForm();
            }
            catch
            {
                // Uh, oh. Something is wrong.
                this.lblStatus.ForeColor = System.Drawing.Color.Red;
                this.lblStatus.Text = "Error: This user could not be added to the database.";
            }
        }

        /// <summary>
        /// Clears all text fields on the form.
        /// </summary>
        private void ClearForm()
        {
            // Clear the form.
            this.txtUsername.Text = String.Empty;
            this.txtPassword.Text = String.Empty;
            this.txtConfirmPassword.Text = String.Empty;
        }

        /// <summary>
        /// Clear the form.
        /// </summary>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Clear the form.
            this.ClearForm();
        }
    }
}