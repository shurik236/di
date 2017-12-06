using System.IO;

namespace TagsCloudVisualization.DocumentReader
{
    class TextFileReader : IDocumentReader
    {
        public string ReadTextFromFile(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
