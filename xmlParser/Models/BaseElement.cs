using System.Text.RegularExpressions;
using xmlParser.Interfaces;

namespace xmlParser
{
    public class BaseElement
    {
        public string Name { get; protected set; }
        public string Content { get; protected set; }
        public List<BaseElement> Children { get; protected set; } = new List<BaseElement>();
        public Dictionary<string, string> Attributes { get; protected set; } = new Dictionary<string, string>();

        public ITagRegexManager TagRegexManager { get; protected set; }


        public BaseElement(string name, string content, List<BaseElement> containerOfElements, ITagRegexManager tagRegexManager, string? attributes = null)
        {
            this.Content = content;
            this.Name = name;
            TagRegexManager = tagRegexManager;

            containerOfElements.Add(this);

            Children.Add(CreateChildrenElement(content, containerOfElements));
            this.Attributes = CalculateAttributes(attributes);
        }

        public Dictionary<string, string> CalculateAttributes(string? attributes)
        {
            Dictionary<string, string> attributesPairs = new Dictionary<string, string>();
            if (attributes != null && attributes.Contains("="))
            {
                foreach (var item in attributes.Split(" "))
                {
                    if (item.Contains("="))
                    {
                        var pair = item.Split("=");
                        attributesPairs.Add(pair.FirstOrDefault(), pair.LastOrDefault());
                    }
                }
            }

            return attributesPairs;
        }

        public int FindReplicaOpenTag(string tagName, string content)
        {
            Regex replicaOpenTeg = TagRegexManager.GetOpenTag(tagName);// new Regex(@"<" + tagName + @"\b[^>]*");
            var replicaOpenTags = replicaOpenTeg.Matches(content);

            return replicaOpenTags.Count;
        }

        public string GetNameFromTag(string rawTag)
        {

            return TagRegexManager.ReplaseForGetName().Replace(rawTag, string.Empty);
        }

        public BaseElement CreateChildrenElement(string content, List<BaseElement> container)
        {
            Regex regexOpenTag = TagRegexManager.GetOpenTag();// new Regex(@"<([^>]+)>");
            var openTag = regexOpenTag.Match(content);

            if (openTag.Value == "")
            {

                return null;
            }

            var tagName = GetNameFromTag(openTag.Value.Trim());

            var attributes = tagName.Contains(' ') ? tagName.Substring(tagName.IndexOf(' ')).Trim() : null;
            tagName = tagName.Split(" ")[0].Trim();

            Regex regexCloseTag = TagRegexManager.GetCloseTag(tagName);// new Regex(@"<\/" + tagName + ">");

            var closeTag = regexCloseTag.Matches(content);

            var start = openTag.Index + openTag.Length;
            var length = closeTag[0].Index - openTag.Length + openTag.Index;
            var tagContent = content.Substring(start, length).Trim();

            var replicaOpen = FindReplicaOpenTag(tagName, tagContent);
            var replicaClose = regexCloseTag.Matches(tagContent);

            while (replicaOpen != replicaClose.Count)
            {
                start = openTag.Index + openTag.Length;
                length = closeTag[replicaOpen].Index - openTag.Length + openTag.Index;
                tagContent = content.Substring(start, length).Trim();

                replicaOpen = FindReplicaOpenTag(tagName, tagContent);
                replicaClose = regexCloseTag.Matches(tagContent);
            }

            if (closeTag[replicaOpen].Index + closeTag[replicaOpen].Length != content.Length)
            {
                CreateChildrenElement(content.Substring(closeTag[replicaOpen].Index + closeTag[replicaOpen].Length).Trim(), container);
            }

            return new BaseElement(tagName, tagContent, container, TagRegexManager, attributes: attributes);
        }

        public string GetUnparseDocument()
        {

            return $"<{Name}>{Content}</{Name}>";
        }

        public override string ToString()
        {

            return Name;
        }
    }
}