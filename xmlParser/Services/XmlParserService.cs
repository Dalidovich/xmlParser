using System.Text;
using xmlParser.Helpers;

namespace xmlParser.Services
{
    public class XmlParserService
    {
        private readonly string _document;
        private readonly BaseElement _element;
        private List<BaseElement> _elementContainer = new List<BaseElement>();

        public static async Task<string> LoadText(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                byte[] buffer = new byte[fs.Length];
                await fs.ReadAsync(buffer, 0, buffer.Length);
                string textFromFile = Encoding.Default.GetString(buffer);
                return textFromFile;
            }
        }

        private XmlParserService(string document)
        {
            _document = document;

            _element = new BaseElement("document", _document, _elementContainer, new XmlTagRegexManager());
        }

        public static async Task<XmlParserService> CreateAsync(string documentPath)
        {
            var document = await LoadText(documentPath);
            var instance = new XmlParserService(document);
            return instance;
        }

        public List<BaseElement> GetParsedElements()
        {
            return _elementContainer;
        }
    }
}