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

        #region " Page Events "

        protected void Page_PreRender(object sender, EventArgs e)
        {
            // Create __doPostBack() definition and hidden input controls.
            ClientScript.GetPostBackEventReference(this, string.Empty);
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            // Initialize filter dropdowns.
            this.InitControls();
        }

        private void InitControls()
        {
            // Populate "Year" dropdown.
            for (int i = 2000; i <= 2050; i++)
            {
                this.ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            // Populate "Month" dropdown.
            for (int j = 1; j <= 12; j++)
            {
                this.ddlMonth.Items.Add(new ListItem(HDateTime.GetMonthName(j), j.ToString()));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Set what month the page should display.
            DateTime displayMonth = this.SetCurrentMonth();

            // Output html to page.
            this.LoadCalendar(displayMonth);

            if (!Page.IsPostBack)
            {
                // First load will be the current month.
                this.SetControls(DateTime.Now);
            }
        }

        #endregion

        #region " Get Methods "

        /// <summary>
        /// Creates the xml containing the calendar month data.
        /// </summary>
        /// <param name="dt">The month that will be translated into xml.</param>
        /// <returns>A memory stream of the created xml.</returns>
        public MemoryStream GetCalendarXml(DateTime dt)
        {
            // Get holidays for the month.
            Dictionary<DateTime, String> holidays = HDateTime.GetHolidaysInMonth(dt);

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

                // Loop through each day in the week until Saturday, which will indicate that tomorrow is the start of a new week.
                DayOfWeek nextDay = DayOfWeek.Monday;
                while (nextDay != DayOfWeek.Sunday && currentDay.Month != startofMonth.AddMonths(1).Month)
                {
                    // Start <currentDay.DayOfWeek>
                    xml.WriteStartElement(currentDay.DayOfWeek.ToString().ToLower());
                    xml.WriteAttributeString("day", currentDay.Day.ToString());

                    if (currentDay.Date.Equals(DateTime.Now.Date))
                    {
                        // This is today's date.
                        xml.WriteAttributeString("today", "true");
                    }

                    // Check if this day is a holiday.
                    foreach (KeyValuePair<DateTime, String> holiday in holidays)
                    {
                        if (holiday.Key.Date.Equals(currentDay.Date))
                        {
                            // Add the holiday to this day.
                            xml.WriteStartElement("event");
                            xml.WriteValue(holiday.Value);
                            xml.WriteEndElement();
                        }
                    }

                    // End <currentDay.DayOfWeek>
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

            // Set memory stream position back to the beginning.
            ms.Position = 0;

            return ms;
        }

        #endregion

        #region " Set Methods "

        /// <summary>
        /// Set UI controls.
        /// </summary>
        /// <param name="displayMonth">The month indicating what is being displayed.</param>
        private void SetControls(DateTime displayMonth)
        {
            this.ddlYear.SelectedValue = displayMonth.Year.ToString();
            this.ddlMonth.SelectedValue = displayMonth.Month.ToString();
        }

        /// <summary>
        /// Determine what month to create xml data for based on prev/next postback.
        /// </summary>
        /// <returns>The month to be displayed on the page.</returns>
        private DateTime SetCurrentMonth()
        {
            DateTime dt = DateTime.Now;
            if (Request.Form["__EVENTTARGET"] == "prev" || Request.Form["__EVENTTARGET"] == "next")
            {
                // Set postback value.
                dt = HDateTime.GetDateTime(Request.Form["__EVENTARGUMENT"]);

                // Set UI to indicate what month is being displayed.
                this.SetControls(dt);
            }

            return dt;
        }

        #endregion

        #region " Load Methods "

        /// <summary>
        /// Output html to display calendar grid.
        /// </summary>
        /// <param name="displayMonth">The month to display.</param>
        public void LoadCalendar(DateTime displayMonth)
        {
            // Create the xml for the indicated month.
            XmlReader inputXml = HXml.GetXmlReader(this.GetCalendarXml(displayMonth));

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

        #endregion

        #region " Click Events "

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Get the month the page should display.
            DateTime displayMonth = HDateTime.GetDateTime(HNumeric.GetSafeInteger(this.ddlYear.SelectedValue), HNumeric.GetSafeInteger(this.ddlMonth.SelectedValue), 1);

            // Set UI to indicate what month is being displayed.
            this.SetControls(displayMonth);

            // Output html to page.
            this.LoadCalendar(displayMonth);
        }

        #endregion
    }
}