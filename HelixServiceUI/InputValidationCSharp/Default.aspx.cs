using HelixService.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace HelixServiceUI.FormValidationCSharp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Check if input fields are valid.
            if (this.IsValidForm())
            {
                // If this was an actual form, you would do stuff here.
            }
        }

        private Boolean IsValidForm()
        {
            Boolean valid = true;

            // Validate e-mail input.
            if (HString.IsValidEmail(this.txtEmail.Text.Trim()))
            {
                this.AddResultControl(this.inputEmail, this.txtEmail.Text.Trim(), true);
            }
            else
            {
                this.AddResultControl(this.inputEmail, this.txtEmail.Text.Trim(), false);
                valid = false;
            }

            // Validate password input.
            if(HString.IsValidPassword(this.txtPassword.Text.Trim()))
            {
                this.AddResultControl(this.inputPassword, this.txtPassword.Text.Trim(), true);
            }
            else
            {
                this.AddResultControl(this.inputPassword, this.txtPassword.Text.Trim(), false);
                valid = false;
            }

            // Validate phone number input.
            if (HString.IsValidPhoneNumber(this.txtTelephoneNo.Text.Trim()))
            {
                this.AddResultControl(this.inputTelephoneNo, this.txtTelephoneNo.Text.Trim(), true);
            }
            else
            {
                this.AddResultControl(this.inputTelephoneNo, this.txtTelephoneNo.Text.Trim(), false);
                valid = false;
            }

            // Validate URL input.
            if (HString.IsValidURL(this.txtWebsite.Text.Trim()))
            {
                this.AddResultControl(this.inputWebsite, this.txtWebsite.Text.Trim(), true);
            }
            else
            {
                this.AddResultControl(this.inputWebsite, this.txtWebsite.Text.Trim(), false);
                valid = false;
            }

            // Validate ZIP code input.
            if (HString.IsValidZIPCode(this.txtZipCode.Text.Trim()))
            {
                this.AddResultControl(this.inputZipCode, this.txtZipCode.Text.Trim(), true);
            }
            else
            {
                this.AddResultControl(this.inputZipCode, this.txtZipCode.Text.Trim(), false);
                valid = false;
            }

            // Validate date input.
            if (HDateTime.IsValidDate(this.txtDate.Text.Trim(), "M/d/yyyy", "MM/dd/yyyy"))
            {
                this.AddResultControl(this.inputDate, this.txtDate.Text.Trim(), true);
            }
            else
            {
                this.AddResultControl(this.inputDate, this.txtDate.Text.Trim(), false);
                valid = false;
            }

            // Validate DOB input.
            if (HDateTime.IsValidDateOfBirth(this.txtDOB.Text.Trim(), "M/d/yyyy", "MM/dd/yyyy"))
            {
                this.AddResultControl(this.inputDOB, this.txtDOB.Text.Trim(), true);
            }
            else
            {
                this.AddResultControl(this.inputDOB, this.txtDOB.Text.Trim(), false);
                valid = false;
            }

            // Validate military time input.
            if (HDateTime.IsValidMilitaryTime(this.txtTimeMilitary.Text.Trim()))
            {
                this.AddResultControl(this.inputTimeMilitary, this.txtTimeMilitary.Text.Trim(), true);
            }
            else
            {
                this.AddResultControl(this.inputTimeMilitary, this.txtTimeMilitary.Text.Trim(), false);
                valid = false;
            }

            // Validate standard time input.
            if (HDateTime.IsValidStandardTime(this.txtTimeStandard.Text.Trim()))
            {
                this.AddResultControl(this.inputTimeStandard, this.txtTimeStandard.Text.Trim(), true);
            }
            else
            {
                this.AddResultControl(this.inputTimeStandard, this.txtTimeStandard.Text.Trim(), false);
                valid = false;
            }

            // Validate number input.
            if (HNumeric.GetNullableInteger(this.txtNumber.Text.Trim()) != null)
            {
                this.AddResultControl(this.inputNumber, this.txtNumber.Text.Trim(), true);
            }
            else
            {
                this.AddResultControl(this.inputNumber, this.txtNumber.Text.Trim(), false);
                valid = false;
            }

            // Validate currency input.
            if (HNumeric.GetNullableCurrency(this.txtCurrency.Text.Trim(), CultureInfo.GetCultureInfo("en-US")) != null)
            {
                this.AddResultControl(this.inputCurrency, this.txtCurrency.Text.Trim(), true);
            }
            else
            {
                this.AddResultControl(this.inputCurrency, this.txtCurrency.Text.Trim(), false);
                valid = false;
            }

            return valid;
        }

        /// <summary>
        /// Place result html at the bottom of input container.
        /// </summary>
        /// <param name="inputContainer">The input container.</param>
        /// <param name="input">The input being validated.</param>
        /// <param name="valid">Success or Failure.</param>
        private void AddResultControl(HtmlGenericControl inputContainer, String input, Boolean valid)
        {
            LiteralControl result = new LiteralControl();

            if (valid)
            {
                result.Text = String.Format("<div class='success-container'><label>{0} IS VALID.</label></div>", input);
            }
            else
            {
                result.Text = String.Format("<div class='error-container'><label>{0} IS NOT VALID.</label></div>", input);
            }

            inputContainer.Controls.Add(result);
        }

        /// <summary>
        /// Clear the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            this.txtDate.Text = String.Empty;
            this.txtDOB.Text = String.Empty;
            this.txtEmail.Text = String.Empty;
            this.txtNumber.Text = String.Empty;
            this.txtPassword.Text = String.Empty;
            this.txtTelephoneNo.Text = String.Empty;
            this.txtTimeMilitary.Text = String.Empty;
            this.txtTimeStandard.Text = String.Empty;
            this.txtWebsite.Text = String.Empty;
            this.txtZipCode.Text = String.Empty;
        }
    }
}