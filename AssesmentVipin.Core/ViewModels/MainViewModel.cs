using AssessmentVipin.Core.DataExporters.Abstraction;
using AssessmentVipin.Core.Helpers.Abstraction;
using AssessmentVipin.Core.Models;
using AssessmentVipin.Core.Services.Abstractions;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace AssessmentVipin.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        private IEmployeeQueryServiceClient employeeServiceClient;
        private IDataExporter dataExporter;
        private IDialogService dialogService;

        private ObservableCollection<Employee> employees;
        private int currentPageIndex;
        private int lastPageIndex;
        private string currentPage;
        private string searchText;
        private string exportFilePath;
        private string statusMessage;

        public ObservableCollection<Employee> Employees
        {
            get => employees;
            set => SetProperty(ref employees, value);
        }

        public string CurrentPage
        {
            get => currentPage;
            set => SetProperty(ref currentPage, value);
        }

        public string SearchText
        {
            get => searchText;
            set => SetProperty(ref searchText, value);
        }
        public string ExportFilePath
        {
            get => exportFilePath;
            set => SetProperty(ref exportFilePath, value);
        }

        public string StatusMessage
        {
            get => statusMessage;
            set => SetProperty(ref statusMessage, value);
        }

        public IAsyncRelayCommand NextPageCommand { get; }
        public IAsyncRelayCommand PreviousPageCommand { get; }
        public IAsyncRelayCommand EmployeeSelectedCommand { get; }
        public IAsyncRelayCommand SearchCommand { get; }
        public IAsyncRelayCommand ClearSearchCommand { get; }
        public IAsyncRelayCommand ExportCommand { get; }
        public IAsyncRelayCommand NewEmployeeCommand { get; }

        public MainViewModel(IEmployeeQueryServiceClient employeeServiceClient, IDataExporter dataExporter, IDialogService dialogService)
        {
            this.employeeServiceClient = employeeServiceClient;
            this.dataExporter = dataExporter;
            this.dialogService = dialogService;

            PageTitle = "Employee Listing";

            NextPageCommand = new AsyncRelayCommand(NextPageAsync);
            PreviousPageCommand = new AsyncRelayCommand(PreviousPageAsync);
            SearchCommand = new AsyncRelayCommand(SearchAsync);
            ClearSearchCommand = new AsyncRelayCommand(ClearSearchAsync);
            ExportCommand = new AsyncRelayCommand(ExportAsync);
            NewEmployeeCommand = new AsyncRelayCommand(NewEmployeeAsync);
        }

        private Task NewEmployeeAsync()
        {
            dialogService.ShowEmployeeDialog();
            return Task.CompletedTask;
        }

        private async Task ExportAsync()
        {
            if (IsBusy)
            {
                return;
            }
            try
            {
                IsBusy = true;
                StatusMessage = "Exporting Data";
                if (!string.IsNullOrEmpty(exportFilePath))
                {
                    var result = await dataExporter.ExportAsync(Employees.ToList(), exportFilePath);
                }

                StatusMessage = $"Exported Data to - {exportFilePath}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ClearSearchAsync()
        {
            SearchText = string.Empty;
            await LoadPagedEmployeeDetails(1);
        }

        private async Task SearchAsync()
        {
            if (IsBusy)
            {
                return;
            }
            try
            {
                IsBusy = true;
                StatusMessage = $"Searching employee";

                await LoadPagedEmployeeDetails(1);

                StatusMessage = $"Search Completed";

            }
            finally
            {
                IsBusy = false;
            }
        }

        public override async Task PageLoaded()
        {
            if (IsBusy)
            {
                return;
            }
            try
            {
                IsBusy = true;
                StatusMessage = $"Loading employee details";
                await LoadPagedEmployeeDetails(1);
            }
            finally
            {
                IsBusy = false;
            }

        }

        private async Task PreviousPageAsync()
        {
            if (IsBusy)
            {
                return;
            }
            try
            {
                IsBusy = true;
                if (currentPageIndex == 1)
                {
                    StatusMessage = $"Already in first page, cannot go back";
                    return;
                }

                currentPageIndex = currentPageIndex - 1;

                await LoadPagedEmployeeDetails(currentPageIndex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task NextPageAsync()
        {
            if (IsBusy)
            {
                return;
            }
            try
            {
                IsBusy = true;
                if (currentPageIndex == lastPageIndex)
                {
                    StatusMessage = $"Already in last page, cannot go forward";
                    return;
                }

                currentPageIndex = currentPageIndex + 1;
                await LoadPagedEmployeeDetails(currentPageIndex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LoadPagedEmployeeDetails(int pageIndex)
        {
            var result = default(EmployeeResponse);
            if (string.IsNullOrEmpty(searchText))
            {
                result = await employeeServiceClient.GetEmployeeWithPage(pageIndex, cancellationTokenSource.Token);
            }
            else
            {
                result = await PerformSearch(pageIndex);

            }

            FillEmployeeResult(result);
            StatusMessage = $"Employee details fetched.";
        }

        private async Task<EmployeeResponse?> PerformSearch(int pageIndex)
        {
            var result = await employeeServiceClient.SearchEmployeeWithName(searchText, pageIndex, cancellationTokenSource.Token);
            return result;
        }

        private void FillEmployeeResult(EmployeeResponse result)
        {
            currentPageIndex = result.CurrentPageIndex;
            lastPageIndex = result.LastPageIndex;

            Employees?.Clear();
            Employees = new ObservableCollection<Employee>(result.Employees);

            CurrentPage = $"{currentPageIndex} / {lastPageIndex}";
        }
    }
}
