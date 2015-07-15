using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace HelixService.Utility
{
    public class HXml
    {
        /// <summary>
        /// Create writer using a memory stream.
        /// This will handle fragment xml data and omit the xml declaration.
        /// </summary>
        /// <param name="xmlMemoryStream">The memory stream to write to.</param>
        /// <returns></returns>
        public static XmlWriter GetXmlWriterFragment(MemoryStream xmlMemoryStream)
        {
            // Create xml writer settings.
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.OmitXmlDeclaration = true;
            xmlWriterSettings.CloseOutput = false;
            xmlWriterSettings.ConformanceLevel = ConformanceLevel.Fragment;

            return XmlWriter.Create(xmlMemoryStream, xmlWriterSettings);
        }

        /// <summary>
        /// Create writer using a memory stream.
        /// This will handle fragment xml data and omit the xml declaration.
        /// </summary>
        /// <param name="xmlMemoryStream">The memory stream to write to.</param>
        /// <returns></returns>
        public static XmlWriter GetXmlWriterDocument(MemoryStream xmlMemoryStream)
        {
            // Create xml writer settings.
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.OmitXmlDeclaration = false;
            xmlWriterSettings.CloseOutput = false;
            xmlWriterSettings.ConformanceLevel = ConformanceLevel.Document;

            return XmlWriter.Create(xmlMemoryStream, xmlWriterSettings);
        }

        /// <summary>
        /// Create reader by loading the xml in memory.
        /// This will ignore whitespace and comments.
        /// </summary>
        /// <param name="xmlMemoryStream">The xml memory stream.</param>
        /// <returns></returns>
        public static XmlReader GetXmlReader(MemoryStream xmlMemoryStream)
        {
            // Create xml reader settings.
            XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
            xmlReaderSettings.IgnoreWhitespace = true;
            xmlReaderSettings.IgnoreComments = true;

            return XmlReader.Create(xmlMemoryStream, xmlReaderSettings);
        }

        /// <summary>
        /// Create reader by loading an xslt file from a URI.
        /// This will ignore whitespace and comments.
        /// </summary>
        /// <param name="xmlFilePath">The location of the .xslt file.</param>
        /// <returns></returns>
        public static XmlReader GetXmlReader(String xmlFilePath)
        {
            // Create xml reader settings.
            XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
            xmlReaderSettings.IgnoreWhitespace = true;
            xmlReaderSettings.IgnoreComments = true;

            return XmlReader.Create(xmlFilePath, xmlReaderSettings);
        }

        /// <summary>
        /// Create XslCompiledTransform object with an xslt stylesheet loaded in a reader.
        /// </summary>
        /// <param name="xml">The xslt stylesheet.</param>
        /// <returns></returns>
        public static XslCompiledTransform GetXslt(XmlReader xml)
        {
            // Create xslt settings.
            XsltSettings xsltSettings = new XsltSettings();
            xsltSettings.EnableScript = true;

            // Load transform.
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(xml, xsltSettings, null);
            return xslt;
        }

        /// <summary>
        /// Create XslCompiledTransform object by loading the xslt stylesheet file from a URI.
        /// </summary>
        /// <param name="xmlFilePath">The xslt stylesheet.</param>
        /// <returns></returns>
        public static XslCompiledTransform GetXslt(String xmlFilePath)
        {
            // Create reader.
            XmlReader xmlReader = XmlReader.Create(xmlFilePath);

            // Create xslt settings.
            XsltSettings xsltSettings = new XsltSettings();
            xsltSettings.EnableScript = true;

            // Load transform.
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(xmlReader, xsltSettings, null);
            return xslt;
        }

        /// <summary>
        /// Transform xml data based on xslt stylesheet, then save the results to a string writer. 
        /// </summary>
        /// <param name="xmlInput">The xml to transform.</param>
        /// <param name="xslTransform">The loaded xslt stylesheet.</param>
        /// <param name="xsltArguments">Xslt arguments for this transform, such as extension objects.</param>
        /// <returns></returns>
        public static StringWriter Transform(XmlReader xmlInput, XslCompiledTransform xslTransform, XsltArgumentList xsltArguments)
        {
            // Create StringWriter to store results.
            StringWriter results = new StringWriter();

            // Transform data using xslt stylesheet.
            xslTransform.Transform(xmlInput, xsltArguments, results);
            return results;
        }

        /// <summary>
        /// Gets the value of the first XmlNode that matches the XPath expression.  
        /// </summary>
        /// <param name="xpn">The cursor for navigating the xml.</param>
        /// <param name="xpath">The path to the node.</param>
        /// <returns></returns>
        public static String SelectNodeValue(XPathNavigator xpn, String xpath)
        {
            XPathNavigator selectNode = xpn.SelectSingleNode(xpath);
            return (selectNode != null) ? selectNode.Value : String.Empty;
        }

        /// <summary>
        /// Process object as XML.
        /// </summary>
        /// <typeparam name="T">The object type to process.</typeparam>
        /// <param name="value">The object to process.</param>
        /// <returns></returns>
        public static String SerializeToXmlString<T>(T value)
        {
            String xmlResult = String.Empty;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringWriter writer = new StringWriter(CultureInfo.InvariantCulture))
            {
                serializer.Serialize(writer, value);
                xmlResult = writer.ToString();
            }

            return xmlResult;
        }

        /// <summary>
        /// Process object as XML.
        /// </summary>
        /// <typeparam name="T">The object type to process.</typeparam>
        /// <param name="value">The object to process.</param>
        /// <returns></returns>
        public static XElement SerializeToXml<T>(T value)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            XDocument doc = new XDocument();

            using (XmlWriter xw = doc.CreateWriter())
            {
                xs.Serialize(xw, value);
                xw.Close();
            }

            XElement element = doc.Root;
            return element;
        }

        /// <summary>
        /// Process XML as object.
        /// </summary>
        /// <typeparam name="T">The object type to deserialize to.</typeparam>
        /// <param name="xml">The xml to deserialize.</param>
        /// <returns></returns>
        public static T DeserializeFromXml<T>(String xml)
        {
            T deserialized = default(T);
            XElement element = XElement.Parse(xml);
            XmlSerializer xs = new XmlSerializer(typeof(T));

            using (XmlReader xr = element.CreateReader())
            {
                deserialized = (T)xs.Deserialize(xr);
                xr.Close();
            }

            return deserialized;
        }
    }
}