using Microsoft.VisualBasic;
using System;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace xmlParser
{
    public partial class Program
    {
        public class ObjectDetective<T> where T : class
        {
            public readonly Type type = typeof(T);

            public string GetName() => type.Name;

            public List<T> GetListElements(bool liteFilter, List<Element> elements)
            {
                var ar = elements
                    .Where(x => x.name == GetName())
                    .Where(x=>x.children.Count==type.GetProperties().Length || liteFilter)
                    .ToArray();

                var List = new List<T>();
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(TestModel));

                for (int i = 0; i < ar.Length; i++)
                {
                    var strElement = $"<{GetName()}>{ar[i].content}</{GetName()}>";

                    byte[] byteArray = Encoding.UTF8.GetBytes(strElement);
                    MemoryStream stream = new MemoryStream(byteArray);

                    var deserializeModel = xmlSerializer.Deserialize(stream) as T;

                    if (deserializeModel!=null)
                    {
                        List.Add(deserializeModel);
                    }
                }

                return List;
            }
        }
    }
}