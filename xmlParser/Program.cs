using System;
using System.Text;
using System.Xml.Serialization;

namespace xmlParser
{
    public partial class Program
    {
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

        public static void Serialaze()
        {
            var innerTestModel = new InnerTestModel()
            {
                name= "Test",
                mail="testmail"
            };
            var testModel = new TestModel()
            {
                mail="mail@mail.com",
                Name="ilia",
                age=19,
                InnerTestModel=innerTestModel
            };
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(TestModel));
            using (FileStream fs = new FileStream("testModel.xml", FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, testModel);

                Console.WriteLine("Object has been serialized");
            }

            using (FileStream fs = new FileStream("testModel.xml", FileMode.OpenOrCreate))
            {
                TestModel? deserializeModel = xmlSerializer.Deserialize(fs) as TestModel;

                if (deserializeModel.Name == testModel.Name)
                {
                    Console.WriteLine("Good");
                }
            }
        }

        static async Task Main(string[] args)
        {
            //Serialaze();

            var textPath = "test.xml";
            var text = await LoadText(textPath);

            var list = new List<Element>();

            var doc = new Element("document", text,list);

            var det=new ObjectDetective<TestModel>();

            det.GetListElements(false,list);
        }
    }
}