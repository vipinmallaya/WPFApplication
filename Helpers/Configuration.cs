using AssessmentVipin.Core.Helpers.Abstraction;
using System.Configuration;

namespace AssessmentVipin.Helpers
{
    public class Configuration : IConfiguration
    {
        public string BaseUrl
        {
            get;
            private set;
        }

        public string Token
        {
            get;
            private set;
        }

        public Configuration()
        {
            BaseUrl = ConfigurationManager.AppSettings["BaseUrl"].ToString();
            Token = ConfigurationManager.AppSettings["Token"].ToString();
        }
    }
}
