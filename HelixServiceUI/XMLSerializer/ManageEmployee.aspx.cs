using HelixService.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HelixServiceUI.XMLSerializer
{
    public partial class ManageEmployee : System.Web.UI.Page
    {

        #region " Properties "

        /// <summary>
        /// The title of the page.
        /// </summary>
        public String PageTitle { get; set; }

        /// <summary>
        /// The identifier for the employee being modified.
        /// </summary>
        private Guid EmployeeGuid
        {
            get
            {
                Guid guid = Guid.Empty;
                Guid.TryParse(HString.SafeTrim(Request.QueryString["eid"]), out guid);
                return guid;
            }
        }

        /// <summary>
        /// The employee being created/modified.
        /// </summary>
        private Employee CurrentEmployee
        {
            get
            {
                if (ViewState["ManageEmployee_Current"] == null)
                {
                    if (this.EmployeeGuid != null && this.EmployeeGuid != Guid.Empty)
                    {
                        ViewState["ManageEmployee_Current"] = Employee.Load(new EmployeeFilter() { Guid = this.EmployeeGuid });
                    }
                }
                return ViewState["ManageEmployee_Current"] as Employee;
            }
            set
            {
                ViewState["ManageEmployee_Current"] = value;
            }
        }

        #endregion

        #region " Page Events "

        /// <summary>
        /// Page load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // FIRST LOAD

                if (this.CurrentEmployee != null)
                {
                    // EXISTING
                    this.PageTitle = "Edit Employee";
                    this.txtFirstName.Text = this.CurrentEmployee.FirstName;
                    this.txtLastName.Text = this.CurrentEmployee.LastName;
                    this.txtEmail.Text = this.CurrentEmployee.Email;
                    this.txtPhone.Text = this.CurrentEmployee.Phone;
                    this.txtState.Text = this.CurrentEmployee.State;
                    this.txtCity.Text = this.CurrentEmployee.City;
                    this.txtStreet.Text = this.CurrentEmployee.Street;
                    this.txtZip.Text = this.CurrentEmployee.Zip;
                    this.divError.InnerText = this.CurrentEmployee.Xml.ToString();
                }
                else
                {
                    // NEW
                    this.PageTitle = "Add Employee";
                    this.CurrentEmployee = new Employee();
                }
            }

            this.divSuccess.Visible = false;
            this.divError.Visible = false;
        }

        /// <summary>
        /// Save current employee.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Set values.
            this.CurrentEmployee.FirstName = HString.SafeTrim(this.txtFirstName.Text);
            this.CurrentEmployee.LastName = HString.SafeTrim(this.txtLastName.Text);
            this.CurrentEmployee.Email = HString.SafeTrim(this.txtEmail.Text);
            this.CurrentEmployee.Phone = HString.SafeTrim(this.txtPhone.Text);
            this.CurrentEmployee.State = HString.SafeTrim(this.txtState.Text);
            this.CurrentEmployee.City = HString.SafeTrim(this.txtCity.Text);
            this.CurrentEmployee.Street = HString.SafeTrim(this.txtStreet.Text);
            this.CurrentEmployee.Zip = HString.SafeTrim(this.txtZip.Text);

            if (this.IsValidEmployee())
            {
                // Save employee.
                this.CurrentEmployee.Commit();

                this.divSuccess.Visible = true;
                this.divSuccess.InnerHtml = String.Format("<div>You have successfully {0} an employee.</div>", CurrentEmployee.ObjectState == ObjectState.ToBeInserted ? "added" : "updated");

                if (this.CurrentEmployee.ObjectState == ObjectState.ToBeInserted)
                {
                    // Allow for additional employees to be added.
                    this.CurrentEmployee = new Employee();
                    this.ClearForm();
                }
            }
        }

        /// <summary>
        /// Abort page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(this.ResolveUrl("~/XMLSerializer/Default.aspx"));
        }

        #endregion

        #region " Helper Methods "

        /// <summary>
        /// Validate certain fields of current employee.
        /// </summary>
        /// <returns></returns>
        private Boolean IsValidEmployee()
        {
            this.divError.InnerHtml = String.Empty;

            if (!(this.CurrentEmployee.FirstName.Length > 0))
            {
                this.divError.InnerHtml += "<div>First Name is required.</div>";
            }

            if (!(this.CurrentEmployee.LastName.Length > 0))
            {
                this.divError.InnerHtml += "<div>Last Name is required.</div>";
            }

            if (!(this.CurrentEmployee.Email.Length > 0))
            {
                this.divError.InnerHtml += "<div>Email is required.</div>";
            }
            else if (!HString.IsValidEmail(this.CurrentEmployee.Email))
            {
                this.divError.InnerHtml += "<div>Email is invalid.</div>";
            }

            if (!(this.CurrentEmployee.Phone.Length > 0))
            {
                this.divError.InnerHtml += "<div>Phone is required.</div>";
            }
            else if (!HString.IsValidPhoneNumber(this.CurrentEmployee.Phone))
            {
                this.divError.InnerHtml += "<div>Phone is invalid.</div>";
            }

            if (!(this.CurrentEmployee.State.Length > 0))
            {
                this.divError.InnerHtml += "<div>State is required.</div>";
            }

            if (!(this.CurrentEmployee.City.Length > 0))
            {
                this.divError.InnerHtml += "<div>City is required.</div>";
            }

            if (!(this.CurrentEmployee.Street.Length > 0))
            {
                this.divError.InnerHtml += "<div>Street is required.</div>";
            }

            if (!(this.CurrentEmployee.Zip.Length > 0))
            {
                this.divError.InnerHtml += "<div>Zip is required.</div>";
            }
            else if (!HString.IsValidZIPCode(this.CurrentEmployee.Zip))
            {
                this.divError.InnerHtml += "<div>Zip is invalid.</div>";
            }

            this.divError.Visible = true;
            return !(this.divError.InnerHtml.Length > 0);
        }

        /// <summary>
        /// Clear all input fields.
        /// </summary>
        private void ClearForm()
        {
            this.txtFirstName.Text = String.Empty;
            this.txtLastName.Text = String.Empty;
            this.txtEmail.Text = String.Empty;
            this.txtPhone.Text = String.Empty;
            this.txtState.Text = String.Empty;
            this.txtCity.Text = String.Empty;
            this.txtStreet.Text = String.Empty;
            this.txtZip.Text = String.Empty;
        }

        #endregion
    }
}