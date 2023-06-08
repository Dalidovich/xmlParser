using xmlParser.Models;
using xmlParser.Services;

namespace xmlParser
{
    public partial class Program
    {
        static async Task Main(string[] args)
        {
            var parserService = await XmlParserService.CreateAsync("test.xml");
            var elements = parserService.GetParsedElements();

            var detective = new ObjectDetectiveService<TestModel>(elements, true);
            var models = detective.GetListObjectElements().ToArray();
        }
    }
}