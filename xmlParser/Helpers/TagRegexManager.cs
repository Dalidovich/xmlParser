using System.Text.RegularExpressions;
using xmlParser.Interfaces;

namespace xmlParser.Helpers
{
    public class XmlTagRegexManager : ITagRegexManager
    {
        public Regex GetOpenTag(string tagName = null)
        {
            if (tagName == null)
            {

                return new Regex(@"<([^>]+)>");
            }

            return new Regex(@"<" + tagName + @"\b[^>]*");
        }

        public Regex GetCloseTag(string openTagName)
        {

            return new Regex(@"<\/" + openTagName + ">");
        }

        public Regex ReplaseForGetName()
        {

            return new Regex("<*>*");
        }
    }
}