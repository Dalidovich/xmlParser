using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
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