using HelixService.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;

namespace HelixServiceUI.CalendarXSLT
{
    public partial class Default : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the month the page should display.
            DateTime displayMonth = this.GetCurrentMonth();

            // Create the xml for the indicated month.
            XmlReader inputXml = HXml.GetXmlReader(this.CreateCalendarXml(displayMonth));

            // Set the xslt stylesheet for this transform.
            XslCompiledTransform transformXml = HXml.GetXslt(Server.MapPath("~/CalendarXSLT/Calendar.xslt"));

            // Extend functionality of xslt stylesheet using "HDateTime" utility object. 
            XsltArgumentList args = new XsltArgumentList();
            args.AddExtensionObject("urn:dt", new HDateTime());

            // Transform the month xml based on xslt style sheet and save it to a string writer.
            StringWriter writer = HXml.Transform(inputXml, transformXml, args);

            // Output the string, which will be html, to the page.
            this.divCalendar.InnerHtml = writer.ToString();
        }

        /// <summary>
        /// Determine what month to create xml data for.
        /// </summary>
        /// <returns>The month to be displayed on the page.</returns>
        private DateTime GetCurrentMonth()
        {
            DateTime dt = DateTime.Now;
            if (Request.Form["__EVENTTARGET"] == "prev" || Request.Form["__EVENTTARGET"] == "next")
            {
                dt = HDateTime.GetDateTime(Request.Form["__EVENTARGUMENT"]);
            }

            return dt;
        }

        /// <summary>
        /// Creates the xml containing the calendar month data.
        /// </summary>
        /// <param name="dt">The month that will be translated into xml.</param>
        /// <returns>A memory stream of the created xml.</returns>
        public MemoryStream CreateCalendarXml(DateTime dt)
        {
            // Create xml writer.
            MemoryStream ms = new MemoryStream();
            XmlWriter xml = HXml.GetXmlWriter(ms);

            // Set the first of the month.
            DateTime startofMonth = HDateTime.FirstOfMonth(dt);

            // Start <year>
            xml.WriteStartElement("year"); 
            xml.WriteAttributeString("value", dt.Year.ToString());

            // Start <month>
            xml.WriteStartElement("month");
            xml.WriteAttributeString("value", dt.Month.ToString());

            // Loop through all the weeks in the month
            for (int i = 0; i < HDateTime.GetNumberOfWeeks(startofMonth, DayOfWeek.Sunday); i++)
            {
                // Set the current day to be the first day in the week, where the first day is a Sunday.
                DateTime currentDay = HDateTime.FirstOfWeek(startofMonth.AddDays(7 * i), DayOfWeek.Sunday);

                if (i == 0)
                {
                    // If this is the beginning of the loop, the first day should always be the first of the current month, which may not be a Sunday.
                    currentDay = startofMonth;
                }

                // Start <week>
                xml.WriteStartElement("week");

                // Loop through each day in the week up till Saturday, which will indicate that tomorrow is the start of a new week.
                DayOfWeek nextDay = DayOfWeek.Monday;
                while (nextDay != DayOfWeek.Sunday && currentDay.Month != startofMonth.AddMonths(1).Month)
                {

                    // Write the current day of the week with the numerical value of the day in the current month.
                    xml.WriteStartElement(currentDay.DayOfWeek.ToString().ToLower());
                    xml.WriteAttributeString("value", currentDay.Day.ToString());
                    xml.WriteEndElement();

                    // Increment the day.
                    currentDay = currentDay.AddDays(1);
                    nextDay = currentDay.DayOfWeek;
                }

                // End <week>
                xml.WriteEndElement();
            }

            // End <month>
            xml.WriteEndElement();

            // End <year>
            xml.WriteEndElement();

            // Close the writer.
            xml.Flush();
            xml.Close();
            ms.Position = 0;

            return ms;
        }
    }
}