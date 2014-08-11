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
    public partial class updateuser : System.Web.UI.Page
    {
        #region " Page Events "

        protected void Page_Init(object sender, EventArgs e)
        {
            // Let's make sure we have a user in session first.
            if (HttpContext.Current.Session["User_LoggedIn"] == null)
                Response.Redirect("~/UserAuthentication/Default.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Do not rebind data on postback, since we do not want any text field values reset.
            if (!Page.IsPostBack)
            {
                // Bind repeater with data source from database.
                this.SetUserTable();
            }
        }

        #endregion

        #region " Data Binding Methods "

        /// <summary>
        /// Bind repeater with data source from database.
        /// </summary>
        private void SetUserTable()
        {
            try
            {
                // Grab all the users from the database.
                List<User> users = UserAuthentication.User.LoadCollection(WebConfigurationManager.AppSettings["ConnString"], new UserFilter());

                // Bind the list of users retrieved.
                this.rUsers.DataSource = users;
                this.rUsers.DataBind();
            }
            catch
            {
                throw new Exception("Error: Unable to load users from database.");
            }
        }

        #endregion

        #region " Handle Repeater Button Click Events "

        /// <summary>
        /// Handle button click events in the repeater.
        /// </summary>
        protected void rUsers_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            // Set user fields from the selected item row.
            User user = new User() { Guid = Guid.Parse(HString.SafeTrim(e.CommandArgument)) };
            user.UserName = ((Label)e.Item.FindControl("lblUserName")).Text;
            user.UserPassword = ((Label)e.Item.FindControl("lblPassword")).Text;
            user.UserSalt = ((HiddenField)e.Item.FindControl("lblSalt")).Value;

            // Do something depending on the button clicked.
            switch (e.CommandName)
            {
                case "Update":
                    this.ToggleDataRow(e, false);
                    break;
                case "Delete":
                    this.DeleteUser(user);
                    break;
                case "Submit":
                    this.UpdateUser(user, e);
                    break;
                case "Cancel":
                    this.ToggleDataRow(e, true);
                    break;
            }
        }

        /// <summary>
        /// Delete the selected user from the repeater.
        /// </summary>
        /// <param name="user">The current user in scope.</param>
        private void DeleteUser(User user)
        {
            try
            {
                // Set this user's state for a delete commit.
                user.State = ObjectState.Delete;

                // User no longer exists. . .
                user.Commit(WebConfigurationManager.AppSettings["ConnString"]);

                // Rebind data.
                this.SetUserTable();
            }
            catch
            {
                throw new Exception("Error: Unable to delete user.");
            }
        }

        /// <summary>
        /// Updates a user's username and/or password.
        /// </summary>
        /// <param name="user">User object to be committed to the database.</param>
        /// <param name="e">The item row being updated.</param>
        private void UpdateUser(User user, RepeaterCommandEventArgs e)
        {
            try
            {
                // Flag that determines when to commit an update.
                Boolean isEdit = false;

                // Set current username to check if we need to commit any changes.
                String currentName = user.UserName;

                // Get username from the corresponding textbox field on this data row.
                user.UserName = HString.SafeTrim(((TextBox)e.Item.FindControl("txtUserName")).Text);

                // If a new password is entered, update it.
                if (((TextBox)e.Item.FindControl("txtPassword")).Text.Length > 0)
                {
                    // Generate a new salt and create a new password hash.
                    user.UserSalt = HCryptography.BytesToHexString(HCryptography.GetRandomSalt(256));
                    user.UserPassword = HCryptography.GetHashString(HString.SafeTrim(((TextBox)e.Item.FindControl("txtPassword")).Text), user.UserSalt, 256);
                    isEdit = true;
                }

                // Check if the username has been modified and that it is unique in the database.
                if (!String.IsNullOrEmpty(user.UserName) && !currentName.Equals(user.UserName) && this.ValidUsername(user.UserName))
                {
                    isEdit = true;
                }

                // Finally, commit any changes, if necessary.
                if (isEdit)
                {
                    // Set this user's state for an update commit.
                    user.State = ObjectState.Update;

                    // Update user credentials.
                    user.Commit(WebConfigurationManager.AppSettings["ConnString"]);

                    // Rebind data.
                    this.SetUserTable();
                }

                // Toggle item row, regardless if we can update user or not.
                this.ToggleDataRow(e, true);
            }
            catch
            {
                throw new Exception("Error: Unable to update user.");
            }
        }

        /// <summary>
        /// Show/Hide controls in the repeater item row.
        /// </summary>
        /// <param name="e">The item row being toggled.</param>
        /// <param name="showActions">Flag to show/hide UI controls.</param>
        private void ToggleDataRow(RepeaterCommandEventArgs e, Boolean showActions)
        {
            // Show/Hide "Update/Delete" actions.
            e.Item.FindControl("divAction").Visible = showActions;

            // Show/Hide the username/password labels.
            e.Item.FindControl("lblUserName").Visible = showActions;
            e.Item.FindControl("lblPassword").Visible = showActions;

            // Show/Hide textboxes to accept edits to current user. 
            e.Item.FindControl("txtUserName").Visible = !showActions;
            e.Item.FindControl("txtPassword").Visible = !showActions;

            // Show/Hide new link buttons to submit changes or cancel them.
            e.Item.FindControl("divCommit").Visible = !showActions;
        }

        /// <summary>
        /// Check if the username is already taken.
        /// </summary>
        /// <param name="name">The username to validate.</param>
        /// <returns></returns>
        private Boolean ValidUsername(String name)
        {
            try
            {
                // Filter users by username.
                UserFilter filter = new UserFilter() { UserName = name };

                // Attempt to load a list of users based on the above filter.
                List<User> users = UserAuthentication.User.LoadCollection(WebConfigurationManager.AppSettings["ConnString"], filter);

                // A valid username is one that does not exist in the database yet.
                if (users.Count == 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}