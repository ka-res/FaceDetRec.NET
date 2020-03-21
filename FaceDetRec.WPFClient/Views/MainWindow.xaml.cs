using System.Windows;
using FaceDetRec.WPFClient.ViewModels.MainWindow;

namespace FaceDetRec.WPFClient.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IMainPageViewModel context)
        {
            InitializeComponent();

            DataContext = context.Get();
        }
    }
}
