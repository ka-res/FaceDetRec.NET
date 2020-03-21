using System.Windows;
using Castle.Windsor;
using Castle.Windsor.Installer;
using FaceDetRec.WPFClient.ViewModels.MainWindow;

namespace FaceDetRec.WPFClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IWindsorContainer _container;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            _container = new WindsorContainer();
            _container.Install(FromAssembly.This());

            var start = _container.Resolve<IShell>();
            start.Run();

            _container.Release(start);
        }
    }
}
