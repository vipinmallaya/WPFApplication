using AssessmentVipin.Core.Models;

namespace AssessmentVipin.Core.Services.Abstractions
{
    public interface IEmployeeQueryServiceClient
    {
        public Task<EmployeeResponse> GetAllEmployees(CancellationToken token);
        public Task<EmployeeResponse> SearchEmployeeWithName(string Name, int pageIndex, CancellationToken token);
        public Task<EmployeeResponse> GetEmployeeWithPage(int pageNumber, CancellationToken token);
    }
}
