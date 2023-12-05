using AssessmentVipin.Core.DataExporters.Abstraction;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace AssessmentVipin.Core.Models
{
    public class Employee : ICsvDataSerialzer
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        public string Serialize()
        {
            return string.Join(',', Id, Name, Email, Gender, Status);
        }

        public string SerializeHeader()
        {
            return string.Join(',', nameof(Id), nameof(Name), nameof(Email), nameof(Gender), nameof(Status));
        }

        public string Validate()
        {
            //TODO::Consider Fluent Validation
            if (string.IsNullOrEmpty(Name) || !Regex.IsMatch(Name, "[A-Za-z., '-]+"))
            {
                return "Invalid name";
            }
            else if (string.IsNullOrEmpty(Email) || !Regex.IsMatch(Email, "[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}"))
            {
                return "Invalid email";
            }
            else if (string.IsNullOrEmpty(Gender) || !(Gender == "Male" || Gender == "Female"))
            {
                return "Invalid gender";
            }
            else if (string.IsNullOrEmpty(Status) || !(Status == "Active" || Status == "Inactive"))
            {
                return "Invalid status";
            }

            return string.Empty;
        }
    }
}
