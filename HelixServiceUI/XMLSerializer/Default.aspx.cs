using HelixService.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HelixServiceUI.XMLSerializer
{
    public partial class Default : System.Web.UI.Page
    {
        #region " Properties "

        /// <summary>
        /// The filter for the employee list.
        /// </summary>
        private EmployeeFilter CurrentFilter
        {
            get
            {
                EmployeeFilter filter = ViewState["ViewEmployees_Filter"] as EmployeeFilter;
                if (filter == null)
                {
                    filter = new EmployeeFilter();
                    ViewState["ViewEmployees_Filter"] = filter;
                }
                return filter;
            }
            set
            {
                ViewState["ViewEmployees_Filter"] = value;
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
            }
        }

        /// <summary>
        /// Logic to run once page is finished loading.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            // Set repeater data source.
            this.rEmployeeList.DataSource = Employee.LoadCollection(this.CurrentFilter);
            this.rEmployeeList.DataBind();
        }

        /// <summary>
        /// Search all employee fields by keyword.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.CurrentFilter.Keyword = HString.SafeTrim(this.txtKeyword.Text);
        }

        /// <summary>
        /// Clear filter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            this.CurrentFilter = new EmployeeFilter();
        }
        
        /// <summary>
        /// Delete the specified employee.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            LinkButton button = sender as LinkButton;
            Guid eid = Guid.Empty;
            Guid.TryParse(HString.SafeTrim(button.CommandArgument), out eid);

            if (eid != Guid.Empty)
            {
                Employee employee = Employee.Load(new EmployeeFilter() { Guid = eid });
                if (employee != null)
                {
                    employee.ObjectState = ObjectState.ToBeDeleted;
                    employee.Commit();
                }
            }
        }

        #endregion
    }
}