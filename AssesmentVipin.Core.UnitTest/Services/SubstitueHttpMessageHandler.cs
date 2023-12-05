using System.Net;

namespace AssessmentVipin.Core.UnitTest.Services
{
    public class SubstitueHttpMessageHandler : HttpMessageHandler
    {
        private string response;
        private HttpStatusCode statusCode;

        public string Input { get; private set; }
        public int NumberOfCalls { get; private set; }

        public SubstitueHttpMessageHandler()
        {

        }

        public void SetMockData(string response, HttpStatusCode statusCode)
        {
            this.response = response;
            this.statusCode = statusCode;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            NumberOfCalls++;
            if (request.Content != null) // Could be a GET-request without a body
            {
                Input = await request.Content.ReadAsStringAsync();
            }

            var returnValues = new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(response)
            };

            returnValues.Headers.Add("x-pagination-pages", new List<string>() { "10" });
            returnValues.Headers.Add("x-pagination-page", new List<string>() { "1" });

            return returnValues;
        }
    }
}
