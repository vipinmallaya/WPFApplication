using AssessmentVipin.Core.Helpers.Abstraction;
using AssessmentVipin.Core.Models;
using AssessmentVipin.Core.Services.Abstractions;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AssessmentVipin.Core.Services
{
    public class EmployeeManagementService : IEmployeeManagementService
    {
        private HttpClient httpclient;
        private string baseAddress;

        public EmployeeManagementService(HttpClient httpclient, IConfiguration configuration)
        {
            this.httpclient = httpclient;
            this.baseAddress = configuration.BaseUrl;

            //TODO::Move to configuration
            this.httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", configuration.Token);
        }

        public async Task CreateNewEmployee(Employee newEmployee, CancellationToken token)
        {
            var employee = JsonSerializer.Serialize(newEmployee);
            var content = new StringContent(employee, Encoding.UTF8, "application/json");

            var result = await httpclient.PostAsync($"{baseAddress}/public/v2/users", content, token);
            result.EnsureSuccessStatusCode();
        }

        public Task<Employee> UpdateEmployeeById(Employee Id, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> DeleteEmployeeById(int Id, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
