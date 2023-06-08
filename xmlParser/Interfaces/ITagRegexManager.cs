using System.Text.RegularExpressions;

namespace xmlParser.Interfaces
{
    public interface ITagRegexManager
    {
        Regex GetCloseTag(string openTagName);
        Regex GetOpenTag(string tagName = null);
        Regex ReplaseForGetName();
    }
}