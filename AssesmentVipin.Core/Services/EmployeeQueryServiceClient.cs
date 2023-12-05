using AssessmentVipin.Core.Helpers.Abstraction;
using AssessmentVipin.Core.Models;
using AssessmentVipin.Core.Services.Abstractions;
using System.Text.Json;

namespace AssessmentVipin.Core.Services
{
    public class EmployeeQueryServiceClient : IEmployeeQueryServiceClient
    {
        private HttpClient httpclient;
        private string baseAddress;

        public EmployeeQueryServiceClient(HttpClient httpclient, IConfiguration configuration)
        {
            this.httpclient = httpclient;
            this.baseAddress = configuration.BaseUrl;
        }

        public async Task<EmployeeResponse> GetAllEmployees(CancellationToken cancellationToken)
        {
            var result = await httpclient.GetAsync($"{baseAddress}/public/v2/users", cancellationToken);
            var employeeResponse = await ProcessEmployeeResponse(result);

            return employeeResponse;
        }

        public async Task<EmployeeResponse> SearchEmployeeWithName(string name, int pageIndex, CancellationToken cancellationToken)
        {
            var result = await httpclient.GetAsync($"{baseAddress}/public/v2/users?name={name}&page={pageIndex}", cancellationToken);
            var employeeResponse = await ProcessEmployeeResponse(result);

            return employeeResponse;
        }

        public async Task<EmployeeResponse> GetEmployeeWithPage(int pageNumber, CancellationToken cancellationToken)
        {
            var result = await httpclient.GetAsync($"{baseAddress}/public/v2/users?page={pageNumber}", cancellationToken);
            var employeeResponse = await ProcessEmployeeResponse(result);

            return employeeResponse;
        }

        private static async Task<EmployeeResponse> ProcessEmployeeResponse(HttpResponseMessage result)
        {
            result.EnsureSuccessStatusCode();

            var employeeResponse = new EmployeeResponse();
            var response = await result.Content.ReadAsStringAsync();
            var employeess = JsonSerializer.Deserialize<List<Employee>>(response);

            employeeResponse.Employees = employeess;
            result.Headers.TryGetValues("x-pagination-pages", out var pagesValues);
            result.Headers.TryGetValues("x-pagination-page", out var currentPage);

            employeeResponse.CurrentPageIndex = int.Parse(currentPage.FirstOrDefault() ?? "0");
            employeeResponse.LastPageIndex = int.Parse(pagesValues.FirstOrDefault() ?? "0");

            return employeeResponse;
        }
    }
}
