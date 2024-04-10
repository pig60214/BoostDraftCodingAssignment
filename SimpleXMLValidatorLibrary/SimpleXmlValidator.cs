namespace SimpleXMLValidatorLibrary
{
    public class SimpleXmlValidator
    {
        //Please implement this method
        public static bool DetermineXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return false;
            var stack = new Stack<string>(); // Store the tags that require their corresponding pair tags.
            var content = xml;

            var hasRootTag = false;
    
            while (!string.IsNullOrEmpty(content))
            {
                // Find the indices of left and right angle brackets.
                var leftBracket = content.IndexOf("<", StringComparison.Ordinal);
                var rightBracket = content.IndexOf(">", StringComparison.Ordinal);
                
                // If no left and right brackets are found, means there is already no tag in the content.
                // So break the loop.
                if (leftBracket == -1 && rightBracket == -1)
                {
                    break;
                }
                
                // If right bracket appears before left bracket(like "><")
                // or left bracket is not found(like ">")
                // or right bracket is not found(like "<"),
                // it means the bracket(s) can't form a tag.
                // So return false.
                if (rightBracket < leftBracket || leftBracket == -1 || rightBracket == -1)
                {
                    return false;
                }
                
                // Extract the tag from content by the indices.
                var tag = GetTag(content, leftBracket, rightBracket);
                
                // Check if stack is not empty
                // It means there are still tags awaiting their pair tags.
                if (stack.Count > 0)
                {
                    // If current tag pairs with the top tag in stack, means the top tag finds its pair tag.
                    // So remove the top tag from stack.
                    if (DeterminePairTags(stack.Peek(), tag))
                    {
                        stack.Pop();
                    }
                    else
                    {
                        // If not paired, means this tag also requires its corresponding pair tag.
                        // So push the current tag onto the stack.
                        stack.Push(tag);
                    }
                }
                else
                {
                    // If stack is empty, it means current tag is a root tag.
                    // XML doesn't allow two root tags.
                    // So, if a root tag has already been encountered, this content is invalid and return false.
                    if (hasRootTag)
                    {
                        return false;
                    }

                    // Set hasRootTag to true to indicate that a root tag has been encountered.
                    hasRootTag = true;
                    stack.Push(tag);
                }
                // Remove the processed tag from the content.
                content = content.Substring(rightBracket + 1, content.Length - rightBracket - 1);
            }
            
            // Check If the xml has root tag and if all tags have their pair tags.
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