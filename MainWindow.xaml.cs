using AssessmentVipin.Core.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using System.Windows;

namespace AssessmentVipin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel dataContext;

        public MainWindow()
        {
            InitializeComponent();
            dataContext = App.Current.Services.GetService<MainViewModel>();

            DataContext = dataContext;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await dataContext.PageLoaded();
        }

        private async void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            await dataContext.SearchCommand.ExecuteAsync(default(object));
        }

        private async void ExportCsv_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new SaveFileDialog();

            fileDialog.Title = "Export CSV";
            fileDialog.AddExtension = true;
            fileDialog.DefaultExt = "csv";
            fileDialog.Filter = "Comma Separated Values (*.csv)|*.csv|All files (*.*)|*.*";
            fileDialog.FilterIndex = 1;

            fileDialog.ShowDialog();

            dataContext.ExportFilePath = fileDialog.FileName;

            await dataContext.ExportCommand.ExecuteAsync(default(object));
        }
    }
}
