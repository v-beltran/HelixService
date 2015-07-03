using HelixService.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace HelixServiceUI.SearchAJAX
{
    /// <summary>
    /// Summary description for Search
    /// </summary>
    public class Search : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            // Respond with the list of states found with search terms.
            context.Response.Write(this.GetStates(context.Request.Params["Search"]));
        }

        [WebMethod]
        public String GetStates(String searchTerm)
        {
            // Create JavaScript Serializer object.
            JavaScriptSerializer jss = new JavaScriptSerializer();

            // Filter by state code and name.
            StateFilter filter = new StateFilter() { Code = searchTerm, Name = searchTerm };

            // Load the states into a list.
            List<State> states = State.LoadCollection(filter);

            // Return with a JSON string containing results.
            return jss.Serialize(states);
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