using System.Text;

namespace xmlParser
{
    public class Program
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

        static async Task Main(string[] args)
        {
            var textPath = "test.xml";
            var text = await LoadText(textPath);

            var doc = new Element("document", text);
        }
    }
}