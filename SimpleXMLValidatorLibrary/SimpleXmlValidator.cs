﻿namespace SimpleXMLValidatorLibrary
{
    public class SimpleXmlValidator
    {
        //Please implement this method
        public static bool DetermineXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return true;
            
            var startOfOpeningTag = xml.IndexOf("<", StringComparison.Ordinal);
            var endOfOpeningTag = xml.IndexOf(">", StringComparison.Ordinal);
            var startOfClosingTag = xml.LastIndexOf("<", StringComparison.Ordinal);
            var endOfClosingTag = xml.LastIndexOf(">", StringComparison.Ordinal);
            
            if (startOfOpeningTag == -1 && endOfOpeningTag == -1 && startOfClosingTag == -1 && endOfClosingTag == -1)
            {
                return true;
            }
            if (startOfOpeningTag > 0 || endOfClosingTag != xml.Length - 1)
            {
                return false;
            }
            
            var openingTag = GetTag(xml, startOfOpeningTag, endOfOpeningTag);
            var closingTag = GetTag(xml, startOfClosingTag, endOfClosingTag);
            if (openingTag.Insert(1, "/") == closingTag)
            {
                return DetermineXml(xml.Substring(endOfOpeningTag + 1, startOfClosingTag - endOfOpeningTag - 1));
            }

            return false;
        }

        private static string GetTag(string xml, int start, int end)
        {
            return xml.Substring(start, end - start + 1);
        }
    }
}