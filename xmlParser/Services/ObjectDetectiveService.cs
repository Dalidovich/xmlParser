using System.Text;
using System.Xml.Serialization;
using xmlParser.Interfaces;
using xmlParser.Models;

namespace xmlParser.Services
{
    public class ObjectDetectiveService<T> : IObjectDetectiveService<T> where T : class
    {
        public readonly Type type = typeof(T);
        private readonly bool _liteFilter;
        private readonly IEnumerable<BaseElement> _container;

        public ObjectDetectiveService(IEnumerable<BaseElement> container, bool liteFilter = false)
        {
            _liteFilter = liteFilter;
            _container = container;
        }

        public List<T> GetListObjectElements()
        {
            var satisfying = GetListSatisfactedElements().ToArray();
            var List = new List<T>();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            for (int i = 0; i < satisfying.Length; i++)
            {
                var strElement = satisfying[i].GetUnparseDocument();

                byte[] byteArray = Encoding.UTF8.GetBytes(strElement);
                MemoryStream stream = new MemoryStream(byteArray);

                var deserializeModel = xmlSerializer.Deserialize(stream) as T;

                if (deserializeModel != null)
                {
                    List.Add(deserializeModel);
                }
            }

            return List;
        }

        public IEnumerable<BaseElement> GetListSatisfactedElements()
        {
            var satisfying = _container
                .Where(x => x.Name == type.Name)
                .Where(x => x.Children.Count == type.GetProperties().Length || _liteFilter);

            return satisfying;
        }
    }
}