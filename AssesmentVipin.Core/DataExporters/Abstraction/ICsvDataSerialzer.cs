namespace AssessmentVipin.Core.DataExporters.Abstraction
{
    public interface ICsvDataSerialzer
    {
        public string Serialize();
        public string SerializeHeader();
    }
}
