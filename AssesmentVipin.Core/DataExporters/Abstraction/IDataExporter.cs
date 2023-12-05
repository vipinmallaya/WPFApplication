namespace AssessmentVipin.Core.DataExporters.Abstraction
{
    public interface IDataExporter
    {
        public Task<bool> ExportAsync<T>(List<T> dataToExport, string fullFilePath) where T : ICsvDataSerialzer;
    }
}
