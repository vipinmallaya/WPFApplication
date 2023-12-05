using AssessmentVipin.Core.Helpers.Abstraction;
using AssessmentVipin.Core.Models;
using AssessmentVipin.Core.Services.Abstractions;
using CommunityToolkit.Mvvm.Input;

namespace AssessmentVipin.Core.ViewModels
{
    public class NewEmployeeViewModel : BaseViewModel
    {
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        private IEmployeeManagementService employeeServiceClient;
        private IDialogService dialogService;

        private string name;
        private string email;
        private string status;
        private string gender;
        private List<string> genderList;
        private List<string> statusList;
        private string validationMessage;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }
        public string Status
        {
            get => status;
            set => SetProperty(ref status, value);
        }

        public string Gender
        {
            get => gender;
            set => SetProperty(ref gender, value);
        }

        public string ValidationMessage
        {
            get => validationMessage;
            set => SetProperty(ref validationMessage, value);
        }

        public List<string> GenderList
        {
            get => genderList;
            set => SetProperty(ref genderList, value);
        }

        public List<string> StatusList
        {
            get => statusList;
            set => SetProperty(ref statusList, value);
        }

        public AsyncRelayCommand SaveCommand { get; }
        public AsyncRelayCommand CancelCommand { get; }

        public NewEmployeeViewModel(IEmployeeManagementService employeeServiceClient, IDialogService dialogService)
        {
            PageTitle = "New Employee";

            this.employeeServiceClient = employeeServiceClient;
            this.dialogService = dialogService;

            SaveCommand = new AsyncRelayCommand(SaveAsync);
            CancelCommand = new AsyncRelayCommand(CancelAsync);

            FillList();
        }

        private void FillList()
        {
            GenderList = new List<string>()
            {
                "Male",
                "Female",
            };

            StatusList = new List<string>()
            {
                "Active",
                "Inactive"
            };
        }

        private Task CancelAsync()
        {
            Name = string.Empty;
            Email = string.Empty;
            Gender = string.Empty;
            Status = string.Empty;

            dialogService.DismissDialog();

            return Task.CompletedTask;
        }

        private async Task SaveAsync()
        {
            try
            {
                ValidationMessage = string.Empty;
                var employee = new Employee()
                {
                    Name = name,
                    Email = email,
                    Gender = gender,
                    Status = status,
                };
                ValidationMessage = employee.Validate();
                if (string.IsNullOrEmpty(ValidationMessage))
                {
                    await employeeServiceClient.CreateNewEmployee(employee, cancellationTokenSource.Token);
                    ValidationMessage = "Employee created";

                }
            }
            catch (Exception)
            {
                ValidationMessage = "Error saving employee information";
            }
        }
    }
}
