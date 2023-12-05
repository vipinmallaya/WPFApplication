using AssessmentVipin.Core.Helpers.Abstraction;

namespace AssessmentVipin.Core.Helpers
{
    public class FileProxy : IFileProxy
    {
        public Task AppendAllLinesAsync(string path, IEnumerable<string> contents)
        {
            return File.AppendAllLinesAsync(path, contents);
        }

        public bool Exists(string fullFilePath)
        {
            return File.Exists(fullFilePath);
        }
    }
}
