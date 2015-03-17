using HelixService.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace HelixServiceUI.DataTransforms.CSVtoXML
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Location of CSV file.
            String csvFile = Server.MapPath("employees.csv");

            // Load file into a stream reader.
            StreamReader reader = new StreamReader(csvFile);

            // Parse the CSV file into an array list.
            ArrayList employees = HList.ToCSVList(reader);

            // Initialize a memory to output xml at the end.
            MemoryStream ms = new MemoryStream();

            using (XmlTextWriter xml = new XmlTextWriter(ms, Encoding.UTF8))
            {
                // Root.
                xml.WriteStartDocument();
                xml.WriteStartElement("employees");

                // Process each employee.
                for (int i = 1; i < employees.Count; i++)
                {
                    List<String> li = employees[i] as List<String>;
                    this.WriteEmployeeData(xml, li);
                }

                // End.
                xml.WriteEndElement();
                xml.WriteEndDocument();
                xml.Flush();
                ms.Position = 0;
            }

            // Set content type and header for output.
            Response.Clear();
            Response.ContentType = "text/xml";
            Response.AddHeader("Content-Disposition", "attachment;filename=employees.xml");
            Response.BinaryWrite(ms.ToArray());
            Response.End();
        }

        /// <summary>
        /// Writes the xml node listing of employee data.
        /// </summary>
        /// <param name="xml">The XML text writer.</param>
        /// <param name="li">The list containing employee data.</param>
        private void WriteEmployeeData(XmlTextWriter xml, List<String> li)
        {
            if (li.Count >= 11)
            {
                // Write employee data.
                xml.WriteStartElement("employee");
                {
                    xml.WriteElementString("id", Guid.NewGuid().ToString());
                    xml.WriteElementString("external-id", li[0]);
                    xml.WriteElementString("name", String.Format("{0} {1}", li[1], li[2]));
                    xml.WriteElementString("title", li[3]);
                    xml.WriteElementString("birthday", li[4]);

                    xml.WriteStartElement("contact");
                    {
                        xml.WriteStartElement("address");
                        {
                            xml.WriteElementString("state", li[5]);
                            xml.WriteElementString("city", li[6]);
                            xml.WriteElementString("street", li[7]);
                            xml.WriteElementString("zip", li[8]);
                        }
                        xml.WriteEndElement();

                        xml.WriteElementString("phone", li[9]);
                        xml.WriteElementString("email", li[10]);
                    }
                    xml.WriteEndElement();
                }
                xml.WriteEndElement();
            }
        }
    }
}