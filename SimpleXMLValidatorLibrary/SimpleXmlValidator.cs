namespace SimpleXMLValidatorLibrary
{
    public class SimpleXmlValidator
    {
        //Please implement this method
        public static bool DetermineXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return false;
            var stack = new Stack<string>();
            var content = xml;

            var hasRootTag = false;

            while (!string.IsNullOrEmpty(content))
            {
                var start = content.IndexOf("<", StringComparison.Ordinal);
                var end = content.IndexOf(">", StringComparison.Ordinal);
                if (start == -1 && end == -1)
                {
                    break;
                }
                if (end < start || start == -1 || end == -1)
                {
                    return false;
                }
                var tag = GetTag(content, start, end);
                if (stack.Count > 0)
                {
                    if (DeterminePairTags(stack.Peek(), tag))
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
                    if (hasRootTag)
                    {
                        return false;
                    }

                    hasRootTag = true;
                    stack.Push(tag);
                }
                content = content.Substring(end + 1, content.Length - end - 1);
            }
            
            return hasRootTag && stack.Count == 0;
        }

        private static bool DeterminePairTags(string openingTag, string closingTag)
        {
            return openingTag.Insert(1, "/") == closingTag;
        }
        
        private static string GetTag(string xml, int start, int end)
        {
            return xml.Substring(start, end - start + 1);
        }
    }
}