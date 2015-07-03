using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace HelixService.Utility
{
    public class HSerialization
    {
        /// <summary>
        /// Process object as XML.
        /// </summary>
        /// <typeparam name="T">The object type to process.</typeparam>
        /// <param name="value">The object to process.</param>
        /// <returns></returns>
        public static String SerializeToXml<T>(T value)
        {
            StringWriter writer = new StringWriter(CultureInfo.InvariantCulture);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(writer, value);
            return writer.ToString();
        }
    }
}