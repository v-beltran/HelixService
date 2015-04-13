using HelixService.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HelixServiceUI.BinaryHandler
{
    public partial class ViewUploads : System.Web.UI.Page
    {
        #region " Properties "

        /// <summary>
        /// Ascending sort order for grid.
        /// </summary>
        private const string ASCENDING = " ASC";

        /// <summary>
        /// Descending sort order for grid.
        /// </summary>
        private const string DESCENDING = " DESC";

        /// <summary>
        /// View-State: The file list used as the data source for grid.
        /// </summary>
        private List<Blob> BlobFileList
        {
            get
            {
                if (ViewState["BlobFileList"] == null)
                {
                    BlobFilter filter = new BlobFilter() { IncludeBinaryData = false };
                    List<Blob> files = Blob.LoadCollection(HConfig.DBConnectionString, filter);
                    ViewState["BlobFileList"] = files;
                }
                return (List<Blob>)ViewState["BlobFileList"];
            }
            set
            {
                ViewState["BlobFileList"] = value;
            }
        }

        /// <summary>
        /// View-State: The sort expression for grid.
        /// </summary>
        private String GridViewSortExpression
        {
            get
            {
                return HString.SafeTrim(ViewState["SortExpression"]);
            }
            set
            {
                ViewState["SortExpression"] = value;
            }
        }

        /// <summary>
        /// View-State: The sort direction for grid.
        /// </summary>
        private SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["SortDirection"] == null)
                    ViewState["SortDirection"] = SortDirection.Ascending;
                return (SortDirection)ViewState["SortDirection"];
            }
            set
            {
                ViewState["SortDirection"] = value;
            }
        }

        #endregion

        #region " Page Events "

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.BlobFileList = null; // Get fresh list of files.
                this.gvUploads.DataSource = this.BlobFileList;
                this.gvUploads.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // Get search filter.
            String searchText = HString.SafeTrim(this.txtSearch.Text);
            BlobFilter filter = new BlobFilter() { IncludeBinaryData = false, Name = searchText };

            // Search files by name.
            List<Blob> files = Blob.LoadCollection(HConfig.DBConnectionString, filter);

            // Rebind results.
            this.BlobFileList = files;
            this.gvUploads.DataSource = this.BlobFileList;
            this.gvUploads.DataBind();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            // Clear current list and rebind with fresh results.
            this.txtSearch.Text = String.Empty;
            this.BlobFileList = null;
            this.gvUploads.DataSource = this.BlobFileList;
            this.gvUploads.DataBind();
        }

        #endregion

        #region " Grid Events "

        /// <summary>
        /// Add custom content to rows.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvUploads_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                // This is the header. See if this column is being sorted on.
                int sortColumnIndex = this.GetSortColumnIndex();
                if (sortColumnIndex != -1)
                {
                    // Index found. Append an arrow to header text.
                    this.AddSortDirection(sortColumnIndex, e.Row);
                }
            }
        }

        /// <summary>
        /// Modify what data gets binded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvUploads_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // This is the current row being binded.
                GridViewRow gridRow = e.Row as GridViewRow;
                if (gridRow.Cells.Count >= 3)
                {
                    // Modify file size value to something friendlier.
                    TableCell fileSize = gridRow.Cells[2];
                    fileSize.Text = HBinary.GetBytesReadable(HNumeric.GetSafeInteger(fileSize.Text));
                }
            }
        }

        /// <summary>
        /// Delete a blob.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvUploads_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32? blobId = HNumeric.GetNullableInteger(this.gvUploads.DataKeys[e.RowIndex].Value);
            if (blobId != null)
            {
                try
                {
                    // Delete blob from database with the specified binary ID.
                    Blob blob = new Blob() { ID = blobId.Value, State = ObjectState.ToBeDeleted };
                    blob.Commit(HConfig.DBConnectionString);

                    // Update grid data source.
                    this.BlobFileList.RemoveAll(x => x.ID.Equals(blobId.Value));
                    this.SortGridView();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error: Failed to delete blob.", ex);
                }
            }
        }

        /// <summary>
        /// Sort the grid view based on selected column.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvUploads_Sorting(object sender, GridViewSortEventArgs e)
        {
            // Save the current sort expression to view state.
            this.GridViewSortExpression = e.SortExpression;

            // Save the current sort direction and sort the grid view.
            if (this.GridViewSortDirection == SortDirection.Ascending)
            {
                // Descending order
                this.GridViewSortDirection = SortDirection.Descending;
                this.SortGridView(DESCENDING);
            }
            else
            {
                // Ascending order
                this.GridViewSortDirection = SortDirection.Ascending;
                this.SortGridView(ASCENDING);
            }
        }

        /// <summary>
        /// Bind the grid view for the selected page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvUploads_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the index of the new page.
            this.gvUploads.PageIndex = e.NewPageIndex;
            
            // Rebind data source, and if necessary, re-sort.
            this.SortGridView();
        }

        #endregion

        #region " Helper Methods "

        /// <summary>
        /// Find the index of the currently sorted column.
        /// </summary>
        /// <returns></returns>
        private Int32 GetSortColumnIndex()
        {
            foreach (DataControlField field in this.gvUploads.Columns)
            {
                // Loop through all the columns until we find the current sort expression.
                if (!String.IsNullOrEmpty(this.GridViewSortExpression) && field.SortExpression == this.GridViewSortExpression)
                {
                    return this.gvUploads.Columns.IndexOf(field);
                }
            }

            return -1;
        }

        /// <summary>
        /// Maintain sort on data source and rebind.
        /// </summary>
        /// <param name="direction">The direction of the sort.</param>
        private void SortGridView(String direction)
        {
            // Get current data source as datatable and define a view.
            DataTable dt = HList.ToDataTable<Blob>(this.BlobFileList);
            DataView dv = new DataView(dt);

            if (!String.IsNullOrEmpty(this.GridViewSortExpression))
            {
                // If necessary, sort data source based on sort expression.
                dv.Sort = this.GridViewSortExpression + direction;
            }

            // Rebind file list.
            this.gvUploads.DataSource = dv;
            this.gvUploads.DataBind();
        }

        /// <summary>
        /// Maintain sort on data source and rebind.
        /// </summary>
        private void SortGridView()
        {
            // Rebind grid while maintaining any sort expression.
            if (this.GridViewSortDirection == SortDirection.Ascending)
            {
                // Ascending order
                this.SortGridView(ASCENDING);
            }
            else
            {
                // Descending order
                this.SortGridView(DESCENDING);
            }
        }

        /// <summary>
        /// Append arrow to header text of column being sorted on.
        /// </summary>
        /// <param name="columnIndex">The index of the column.</param>
        /// <param name="headerRow">The row of the column.</param>
        private void AddSortDirection(Int32 columnIndex, GridViewRow headerRow)
        {
            Literal arrow = new Literal();
            if (this.GridViewSortDirection == SortDirection.Ascending)
            {
                arrow.Text = "&#x25B2;"; // Up arrow
            }
            else
            {
                arrow.Text = "&#x25BC;"; // Down arrow
            }

            // Add the direction to the appropriate header cell.
            headerRow.Cells[columnIndex].Controls.Add(arrow);
        }

        #endregion
    }
}