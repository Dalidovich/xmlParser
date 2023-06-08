using System.Text.RegularExpressions;

namespace xmlParser
{
    public class Element
    {
        public string name;
        public string content;
        public List<Element> children = new List<Element>();
        public Dictionary<string, string> attributes = new Dictionary<string, string>();


        public Element(string name, string content, List<Element> containerOfElements, string? attributes = null)
        {
            this.content = content;
            this.name = name;

            containerOfElements.Add(this);

            CreateChildrenElement(content, containerOfElements);
            CalculateAttributes(attributes);
        }

        public void CalculateAttributes(string? attributes)
        {
            if (attributes != null && attributes.Contains("="))
            {
                foreach (var item in attributes.Split(" "))
                {
                    if (item.Contains("="))
                    {
                        var pair = item.Split("=");
                        this.attributes.Add(pair.FirstOrDefault(), pair.LastOrDefault());
                    }
                }
            }
        }

        public int FindReplicaOpenTag(string tagName, string content)
        {
            Regex replicaOpenTeg = new Regex(@"<" + tagName + @"\b[^>]*");
            var replicaOpenTags = replicaOpenTeg.Matches(content);

            return replicaOpenTags.Count;
        }

        public void CreateChildrenElement(string content, List<Element> container)
        {
            Regex regexOpenTag = new Regex(@"<([^>]+)>");
            var openTag = regexOpenTag.Match(content);

            if (openTag.Value == "")
            {

                return;
            }

            var tagName = openTag.Value.Trim().Trim('<', '>');

            var attributes = tagName.Contains(' ') ? tagName.Substring(tagName.IndexOf(' ')).Trim() : null;
            tagName = tagName.Split(" ")[0].Trim();

            Regex regexCloseTag = new Regex(@"<\/" + tagName + ">");

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

            children.Add(new Element(tagName, tagContent, container, attributes: attributes));

            if (closeTag[replicaOpen].Index + closeTag[replicaOpen].Length != content.Length)
            {
                CreateChildrenElement(content.Substring(closeTag[replicaOpen].Index + closeTag[replicaOpen].Length).Trim(),container);
            }
        }

        public override string ToString()
        {

            return name;
        }
    }
}