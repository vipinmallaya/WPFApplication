using NSubstitute;

namespace AssessmentVipin.Core.UnitTest.DataExporters
{
    public class CsvDataExporterTests
    {
        private IFileProxy fileProxySubstitue;
        private CsvDataExporter csvDataExporter;
        [SetUp]
        public void Setup()
        {
            fileProxySubstitue = Substitute.For<IFileProxy>();

            csvDataExporter = new CsvDataExporter(fileProxySubstitue);
        }

        [TestCase("sdasd", ExpectedResult = false)]
        [TestCase("", ExpectedResult = false)]
        public async Task<bool> WhenEmptyContentOrFileNamePassed_ShouldReturnFalse(string filepath)
        {
            return await csvDataExporter.ExportAsync(new List<Employee>(), filepath);
        }


        [Test]
        public async Task WhenValidContentIsPassed_ShouldRecieveCallToIFileProxy_Exists()
        {
            var filePath = "testFIlePath";
            var content = new List<Employee>()
            {
                new Employee()
                {
                    Name="TestName",
                    Email = "Test@test.com",
                    Gender="Male",
                    Status="Active",
                    Id=12
                }

            };
            fileProxySubstitue.Exists(filePath).Returns(true);

            await csvDataExporter.ExportAsync(content, filePath);

            fileProxySubstitue.Received().Exists("testFIlePath");
        }

        [Test]
        public async Task WhenValidContentIsPassed_ShouldRecieveCallToIFileProxy_AppendFileWithHeader()
        {
            var filePath = "testFIlePath";
            var employeeData = new Employee()
            {
                Name = "TestName",
                Email = "Test@test.com",
                Gender = "Male",
                Status = "Active",
                Id = 12
            };
            var content = new List<Employee>()
            {
                employeeData

            };
            fileProxySubstitue.Exists(filePath).Returns(false);

            await csvDataExporter.ExportAsync(content, filePath);

            await fileProxySubstitue.Received().AppendAllLinesAsync("testFIlePath", Arg.Is<IEnumerable<string>>(x =>
            (x.Contains(employeeData.SerializeHeader()) && x.Contains(employeeData.Serialize()))));
        }

        [Test]
        public async Task WhenValidContentIsPassed_ShouldRecieveCallToIFileProxy_AppendFileWithoutHeader()
        {
            var filePath = "testFIlePath";
            var employeeData = new Employee()
            {
                Name = "TestName",
                Email = "Test@test.com",
                Gender = "Male",
                Status = "Active",
                Id = 12
            };
            var content = new List<Employee>()
            {
                employeeData

            };
            fileProxySubstitue.Exists(filePath).Returns(true);

            await csvDataExporter.ExportAsync(content, filePath);

            await fileProxySubstitue.Received().AppendAllLinesAsync("testFIlePath", Arg.Is<IEnumerable<string>>(x =>
            (!x.Contains(employeeData.SerializeHeader()) && x.Contains(employeeData.Serialize()))));
        }
    }
}
