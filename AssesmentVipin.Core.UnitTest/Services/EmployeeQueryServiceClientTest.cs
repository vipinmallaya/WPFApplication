using AssessmentVipin.Core.Services;
using NSubstitute;
using System.Net;

namespace AssessmentVipin.Core.UnitTest.Services
{
    public class EmployeeQueryServiceClientTest
    {
        private EmployeeQueryServiceClient queryService;
        private SubstitueHttpMessageHandler messageHanderSubstitue;

        private string sampleEmployeeResponse = "[{\"id\":5710526,\"name\":\"Gopaal Mehrotra\",\"email\":\"mehrotra_gopaal@casper.test\",\"gender\":\"male\",\"status\":\"inactive\"}]";

        [SetUp]
        public void Setup()
        {
            IConfiguration configSubstitute = Substitute.For<IConfiguration>();
            configSubstitute.BaseUrl.Returns("http://dummy.com");
            messageHanderSubstitue = new SubstitueHttpMessageHandler();
            HttpClient httpClient = new HttpClient(messageHanderSubstitue);


            queryService = new EmployeeQueryServiceClient(httpClient, configSubstitute);
        }

        [Test]
        public async Task WhenGetAllEmployeesInvoked_ProperEmployeesAreReturned()
        {
            var cancellationToken = CancellationToken.None;
            messageHanderSubstitue.SetMockData(sampleEmployeeResponse, System.Net.HttpStatusCode.OK);

            var response = await queryService.GetAllEmployees(cancellationToken);

            Assert.IsTrue(response.CurrentPageIndex == 1);
            Assert.IsTrue(response.LastPageIndex == 10);

            Assert.That(response.Employees.FirstOrDefault().Name, Is.EqualTo("Gopaal Mehrotra"));

        }

        [Test]
        public void WhenGetAllEmployeesInvoked_CatchException()
        {
            var cancellationToken = CancellationToken.None;
            messageHanderSubstitue.SetMockData(sampleEmployeeResponse, System.Net.HttpStatusCode.Unauthorized);

            HttpRequestException ex = Assert.ThrowsAsync<HttpRequestException>(async () => await queryService.GetAllEmployees(cancellationToken));

            Assert.That(ex.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }
    }
}
