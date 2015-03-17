using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace HelixService.Utility
{
    public class HList
    {

        /// <summary>
        /// Get a list of CSV lines and individual column values.
        /// </summary>
        /// <param name="reader">The stream to read the CSV file.</param>
        /// <returns></returns>
        public static ArrayList ToCSVList(StreamReader reader)
        {
            ArrayList lines = new ArrayList();
            String currentLine = String.Empty;
            using (reader)
            {
                while ((currentLine = reader.ReadLine()) != null)
                {
                    List<String> columns = new List<String>();
                    Int32 pos = 0;

                    while (pos < currentLine.Length)
                    {
                        String value;

                        // Special handling for quoted field.
                        if (currentLine[pos] == '"')
                        {
                            // Skip initial quote.
                            pos++;

                            // Parse quoted value.
                            int start = pos;
                            while (pos < currentLine.Length)
                            {
                                // Test for quote character.
                                if (currentLine[pos] == '"')
                                {
                                    // Found one
                                    pos++;

                                    // If two quotes together, keep one.
                                    // Otherwise, indicates end of value.
                                    if (pos >= currentLine.Length || currentLine[pos] != '"')
                                    {
                                        pos--;
                                        break;
                                    }
                                }
                                pos++;
                            }
                            value = currentLine.Substring(start, pos - start);
                            value = value.Replace("\"\"", "\"");
                        }
                        else
                        {
                            // Parse unquoted value.
                            int start = pos;
                            while (pos < currentLine.Length && currentLine[pos] != ',')
                                pos++;
                            value = currentLine.Substring(start, pos - start);
                        }

                        // Eat up to and including next comma.
                        while (pos < currentLine.Length && currentLine[pos] != ',')
                            pos++;
                        if (pos < currentLine.Length)
                            pos++;

                        columns.Add(value);
                    }

                    lines.Add(columns);

                }
            }

            return lines;
        }
    }
}