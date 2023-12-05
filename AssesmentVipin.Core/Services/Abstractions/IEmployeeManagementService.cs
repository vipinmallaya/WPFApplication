using AssessmentVipin.Core.Models;

namespace AssessmentVipin.Core.Services.Abstractions
{
    public interface IEmployeeManagementService
    {
        Task CreateNewEmployee(Employee newEmployee, CancellationToken token);
        Task<Employee> UpdateEmployeeById(Employee employee, CancellationToken token);
        Task<Employee> DeleteEmployeeById(int Id, CancellationToken token);
    }
}
