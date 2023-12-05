using AssessmentVipin.Core.Helpers.Abstraction;
using AssessmentVipin.Views;
using System.Windows;

namespace AssessmentVipin.Helpers
{
    public class DialogService : IDialogService
    {
        private Window activedialogWindow;

        public void DismissDialog()
        {
            activedialogWindow.Close();
        }

        public void ShowEmployeeDialog()
        {
            activedialogWindow = new NewEmployeeWindow();
            activedialogWindow.ShowDialog();
        }
    }
}
