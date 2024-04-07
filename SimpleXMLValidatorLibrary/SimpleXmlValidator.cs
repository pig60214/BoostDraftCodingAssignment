namespace SimpleXMLValidatorLibrary
{
    public class SimpleXmlValidator
    {
        //Please implement this method
        public static bool DetermineXml(string xml)
        {
            var stack = new Stack<string>();
            var content = xml;
            while (!string.IsNullOrEmpty(content))
            {
                var start = content.IndexOf("<", StringComparison.Ordinal);
                var end = content.IndexOf(">", StringComparison.Ordinal);
                if (end < start)
                {
                    return false;
                }
                var tag = GetTag(content, start, end);
                if (stack.Count > 0)
                {
                    if (stack.Peek().Insert(1, "/") == tag)
                    {
                        stack.Pop();
                    }
                    else
                    {
                        stack.Push(tag);
                    }
                }
                else
                {
                    stack.Push(tag);
                }
                content = content.Substring(end + 1, content.Length - end - 1);
            }
            
            return stack.Count == 0;
        }
        public static bool DetermineXml2(string xml)
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
                return DetermineXml2(xml.Substring(endOfOpeningTag + 1, startOfClosingTag - endOfOpeningTag - 1));
            }

            return false;
        }

        private static string GetTag(string xml, int start, int end)
        {
            return xml.Substring(start, end - start + 1);
        }
    }
}