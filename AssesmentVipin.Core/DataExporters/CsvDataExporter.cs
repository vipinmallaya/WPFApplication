using AssessmentVipin.Core.DataExporters.Abstraction;
using AssessmentVipin.Core.Helpers.Abstraction;

namespace AssessmentVipin.Core.DataExporters
{
    public class CsvDataExporter : IDataExporter
    {
        IFileProxy fileProxy;
        public CsvDataExporter(IFileProxy fileProxy)
        {
            this.fileProxy = fileProxy;
        }

        public async Task<bool> ExportAsync<T>(List<T> dataToExport, string fullFilePath) where T : ICsvDataSerialzer
        {
            if ((!dataToExport?.Any() ?? true) || string.IsNullOrEmpty(fullFilePath))
            {
                return false;
            }

            var csvContent = new List<string>();
            if (!fileProxy.Exists(fullFilePath))
            {
                csvContent.Add(dataToExport.First().SerializeHeader());
            }

            foreach (var item in dataToExport)
            {
                csvContent.Add(item.Serialize());
            }

            await fileProxy.AppendAllLinesAsync(fullFilePath, csvContent);

            return true;
        }
    }
}
