using HelixService.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace HelixServiceUI.BinaryHandler
{
    /// <summary>
    /// Summary description for Binary
    /// </summary>
    public class Binary : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            Int32? id = HNumeric.GetNullableInteger(context.Request.QueryString["id"]);

            if (id != null)
            {
                BlobFilter filter = new BlobFilter() { ID = id.Value, IncludeBinaryData = true };
                Blob file = Blob.Load(HConfig.DBConnectionString, filter);

                if (file != null)
                {
                    context.Response.Clear();
                    context.Response.ContentType = file.MimeType;
                    context.Response.AddHeader("content-disposition", "attachment; filename=" + file.Name);
                    context.Response.BinaryWrite(file.BinaryData.ToArray());
                }
            }

            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}