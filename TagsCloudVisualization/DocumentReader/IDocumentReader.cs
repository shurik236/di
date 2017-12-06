namespace TagsCloudVisualization.DocumentReader
{
    public interface IDocumentReader
    {
        //hell, how do we parse all these .doc and .pdf
        string ReadTextFromFile(string path);
    }
}
