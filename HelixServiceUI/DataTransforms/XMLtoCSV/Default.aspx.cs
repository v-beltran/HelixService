using HelixService.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.XPath;

namespace HelixServiceUI.DataTransforms.XMLtoCSV
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Location of xml file.
            String xmlFile = Server.MapPath("employees.xml");

            // String to hold csv to output at the end.
            StringBuilder csvOutput = new StringBuilder();
            csvOutput.AppendLine("Guid, External Id, Name, Title, Birthday, State, City, Address, Zip, Phone, Email");

            // Create XPathDocument and XPathNavigator for employees.
            XPathDocument doc = new XPathDocument(xmlFile);
            XPathNavigator navigator = doc.CreateNavigator();
            XPathNodeIterator iterator = navigator.Select("employees/employee");            
            
            foreach (XPathNavigator xpn in iterator)
            {
                // Process each employee.
                this.TransformEmployee(xpn, ref csvOutput);
            }

            // Set content type and header for output.
            Response.Clear();
            Response.ContentType = "application/CSV";
            Response.AddHeader("Content-Disposition", "attachment;filename=employees.csv");
            Response.Write(csvOutput.ToString());
            Response.End();
        }

        /// <summary>
        /// Create CSV for each employee.
        /// </summary>
        /// <param name="employee">The employee node.</param>
        /// <param name="transform">The CSV string.</param>
        private void TransformEmployee(XPathNavigator employee, ref StringBuilder transform)
        {
            StringBuilder currentEmployee = new StringBuilder();
            String guid = Guid.NewGuid().ToString();
            String id = HString.CsvSafeTrim(HXml.SelectNodeValue(employee, "id"));
            String firstName = HString.CsvSafeTrim(HXml.SelectNodeValue(employee, "first-name"));
            String lastName = HString.CsvSafeTrim(HXml.SelectNodeValue(employee, "last-name"));
            String title = HString.CsvSafeTrim(HXml.SelectNodeValue(employee, "title"));
            String dateOfBirth = HString.CsvSafeTrim(HXml.SelectNodeValue(employee, "date-of-birth"));
            String contact = this.ProcessContact(employee.SelectSingleNode("contact"));

            // Set CSV line for this employee.
            currentEmployee.AppendFormat("{0},{1},{2} {3},{4},{5},{6}", guid, id, firstName, lastName, title, dateOfBirth, contact);

            // Append to current transform data.
            transform.AppendLine(currentEmployee.ToString());
        }

        /// <summary>
        /// Process contact node.
        /// </summary>
        /// <param name="contact">The contact node.</param>
        /// <returns>The contact information of employee.</returns>
        private String ProcessContact(XPathNavigator contact) 
        {
            StringBuilder currentEmployee = new StringBuilder();

            if (contact != null)
            {
                String phone = HString.CsvSafeTrim(HXml.SelectNodeValue(contact, "phone"));
                String email = HString.CsvSafeTrim(HXml.SelectNodeValue(contact, "email"));
                String address = this.ProcessAddress(contact.SelectSingleNode("address"));
                currentEmployee.AppendFormat("{0},{1},{2}", address, phone, email);
            }

            return currentEmployee.ToString();
        }

        /// <summary>
        /// Process address node.
        /// </summary>
        /// <param name="address">The address node.</param>
        /// <returns>The address of employee.</returns>
        private String ProcessAddress(XPathNavigator address)
        {
            StringBuilder currentEmployee = new StringBuilder();

            if (address != null)
            {
                String state = HString.CsvSafeTrim(HXml.SelectNodeValue(address, "state"));
                String city = HString.CsvSafeTrim(HXml.SelectNodeValue(address, "city"));
                String street = HString.CsvSafeTrim(HXml.SelectNodeValue(address, "street"));
                String zip = HString.CsvSafeTrim(HXml.SelectNodeValue(address, "zip"));
                currentEmployee.AppendFormat("{0},{1},{2},{3}", state, city, street, zip);
            }

            return currentEmployee.ToString();
        }
    }
}