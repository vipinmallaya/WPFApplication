namespace AssessmentVipin.Core.UnitTest.Models
{
    public class EmployeeTests
    {
        private Employee employee;

        [SetUp]
        public void Setup()
        {
            employee = new Employee()
            {
                Id = 1,
                Name = "Test",
                Email = "Test@test.com",
                Gender = "Male",
                Status = "Inactive"
            };
        }

        [Test]
        public void ValidateHeaderCreation_WHenDefault()
        {
            var result = employee.SerializeHeader();
            Assert.That(result, Is.EqualTo("Id,Name,Email,Gender,Status"));
        }

        [Test]
        public void ValidateCreation_WhenValuesArePresent()
        {
            var result = employee.Serialize();
            Assert.That(result, Is.EqualTo("1,Test,Test@test.com,Male,Inactive"));
        }

        [Test]
        public void ValidateValidation_WhenInvalidName_ReturnValidationMessage()
        {
            var newEmployee = new Employee()
            {
                Id = 1,
                Name = "",
                Email = "Test@test.com",
                Gender = "Male",
                Status = "Inactive"
            };
            var result = newEmployee.Validate();
            Assert.That(result, Is.EqualTo("Invalid name"));

            newEmployee = new Employee()
            {
                Id = 1,
                Name = "%^(",
                Email = "Test@test.com",
                Gender = "Male",
                Status = "Inactive"
            };

            result = newEmployee.Validate();
            Assert.That(result, Is.EqualTo("Invalid name"));
        }

        [Test]
        public void ValidateValidation_WhenInvalidEmail_ReturnValidationMessage()
        {
            var newEmployee = new Employee()
            {
                Id = 1,
                Name = "ValidName",
                Email = "",
                Gender = "Male",
                Status = "Inactive"
            };
            var result = newEmployee.Validate();
            Assert.That(result, Is.EqualTo("Invalid email"));

            newEmployee = new Employee()
            {
                Id = 1,
                Name = "ValidName",
                Email = "@test.com",
                Gender = "Male",
                Status = "Inactive"
            };

            result = newEmployee.Validate();
            Assert.That(result, Is.EqualTo("Invalid email"));
        }
        [Test]
        public void ValidateValidation_WhenInvalidGender_ReturnValidationMessage()
        {
            var newEmployee = new Employee()
            {
                Id = 1,
                Name = "ValidName",
                Email = "validemail@email.com",
                Gender = "",
                Status = "Inactive"
            };
            var result = newEmployee.Validate();
            Assert.That(result, Is.EqualTo("Invalid gender"));

            newEmployee = new Employee()
            {
                Id = 1,
                Name = "ValidName",
                Email = "valid@test.com",
                Gender = "Males",
                Status = "Inactive"
            };

            result = newEmployee.Validate();
            Assert.That(result, Is.EqualTo("Invalid gender"));

        }

        [Test]
        public void ValidateValidation_WhenInValidStatus_ReturnValidationMessage()
        {
            var newEmployee = new Employee()
            {
                Id = 1,
                Name = "ValidName",
                Email = "email@email.com",
                Gender = "Male",
                Status = ""
            };
            var result = newEmployee.Validate();
            Assert.That(result, Is.EqualTo("Invalid status"));

            newEmployee = new Employee()
            {
                Id = 1,
                Name = "ValidName",
                Email = "test@test.com",
                Gender = "Male",
                Status = "Inactives"
            };

            result = newEmployee.Validate();
            Assert.That(result, Is.EqualTo("Invalid status"));
        }

        [Test]
        public void ValidValidation()
        {
            var newEmployee = new Employee()
            {
                Id = 1,
                Name = "ValidName",
                Email = "test@test.com",
                Gender = "Male",
                Status = "Inactive"
            };

            var result = newEmployee.Validate();
            Assert.IsEmpty(result);
        }
    }
}
