namespace AssessmentVipin.Core.Helpers.Abstraction
{
    public interface IFileProxy
    {
        Task AppendAllLinesAsync(string path, IEnumerable<string> contents);
        bool Exists(string fullFilePath);
    }
}
