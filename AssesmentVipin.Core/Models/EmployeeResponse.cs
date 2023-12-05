namespace AssessmentVipin.Core.Models
{
    public class EmployeeResponse
    {
        public List<Employee> Employees { get; set; }

        public int CurrentPageIndex { get; set; }
        public int LastPageIndex { get; set; }
    }
}
