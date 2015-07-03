using HelixService.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HelixServiceUI.BinaryHandler
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Upload files on submit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            List<Blob> blobs = new List<Blob>();
            Int32 count = 0;

            // Clear previous results.
            this.lSuccess.Text = String.Empty;
            this.lFailure.Text = String.Empty;

            if (this.fuMultipleFiles.HasFiles)
            {
                // If user selected 1 or more files, process them.
                foreach (HttpPostedFile file in this.fuMultipleFiles.PostedFiles)
                {
                    // Add to list of blobs to be uploaded.
                    this.AddBlob(blobs, file);
                }
            }

            if (blobs.Count > 0)
            {
                foreach (Blob b in blobs)
                {
                    try
                    {
                        // Insert into database and inform user.
                        b.Commit();
                        this.lSuccess.Text += String.Format("<p>SUCCESS: {0} uploaded successfully.</p>", b.Name);
                        count++;
                    }
                    catch
                    {
                        // Something bad happened. Tell the user which file failed.
                        this.lFailure.Text += String.Format("<p>ERROR: {0} failed to upload.</p>", b.Name);
                    }
                }
            }

            // Pre-prend simple recap of files uploaded.
            this.lSuccess.Text = String.Format("<p>SUMMARY: {0} file(s) uploaded.</p>", count) + this.lSuccess.Text;
        }

        /// <summary>
        /// Add blob to list that will be uploaded.
        /// </summary>
        /// <param name="blobs">Current list of files.</param>
        /// <param name="file">Current file to be added to list.</param>
        private void AddBlob(List<Blob> blobs, HttpPostedFile file)
        {
            Blob b = new Blob(file);
            if (b.Size <= 20971520)
            {
                // Add to list when file is less than or equal to 20 MB.
                blobs.Add(b);
            }
            else
            {
                this.lFailure.Text += String.Format("<p>ERROR: {0} is larger than 20 MB. This file will not be uploaded.</p>", b.Name);
            }
        }
    }
}