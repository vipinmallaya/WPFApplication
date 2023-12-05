using AssessmentVipin.Core.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace AssessmentVipin.Views
{
    /// <summary>
    /// Interaction logic for NewEmployeeWindow.xaml
    /// </summary>
    public partial class NewEmployeeWindow : Window
    {
        public NewEmployeeWindow()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetService<NewEmployeeViewModel>();
        }
    }
}
