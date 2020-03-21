using Castle.MicroKernel.Registration;
using FaceDetRec.WPFClient.DataBase;
using FaceDetRec.WPFClient.Repositories.Implementations.DataBase;
using FaceDetRec.WPFClient.Repositories.Implementations.LocalStorage;
using FaceDetRec.WPFClient.Repositories.Interfaces.DataBase;
using FaceDetRec.WPFClient.Repositories.Interfaces.LocalStorage;
using FaceDetRec.WPFClient.Services.Implementations;
using FaceDetRec.WPFClient.Services.Interfaces;
using FaceDetRec.WPFClient.ViewModels.MainWindow;
using FaceDetRec.WPFClient.Views;

namespace FaceDetRec.WPFClient.DependencyInjection
{
    public class Installers : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container,
            Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {

            container.Register(Component.For<IControls>()
                .ImplementedBy<Controls>());

            container.Register(Component.For<IParameters>().
                ImplementedBy<Parameters>()
                .LifestyleSingleton());

            container.Register(Component.For<IMethods>().
                ImplementedBy<Methods>().LifestyleSingleton());

            container.Register(Component.For<IFaceRecogntionService>()
                .ImplementedBy<FaceRecognitionService>());

            container.Register(Component.For<IFileDirectoryService>()
                .ImplementedBy<FileDirectoryService>());

            container.Register(Component.For<IPersonRepositoryDb>().
                ImplementedBy<PersonRepositoryDb>());

            container.Register(Component.For<IImageRepositoryDb>()
                .ImplementedBy<ImageRepositoryDb>());

            container.Register(Component.For<IPersonRepositoryLs>().
                ImplementedBy<PersonRepositoryLs>());

            container.Register(Component.For<IImageRepositoryLs>().
                ImplementedBy<ImageRepositoryLs>());

            container.Register(Component.For<ICameraCaptureService>().
                ImplementedBy<CameraCaptureService>());

            container.Register(Component.For<ILocalStorageService>().
                ImplementedBy<LocalStorageService>());

            container.Register(Component.For<IFaceDetectionService>()
                .ImplementedBy<FaceDetectionService>());

            container.Register(Component.For<IDatabaseService>()
                .ImplementedBy<DatabaseService>());

            container.Register(Component.For<IMainPageModel>()
                .ImplementedBy<MainPageModel>());

            container.Register(Component.For<IMainPageViewModel>().
                ImplementedBy<MainPageViewModel>());

            container.Register(Component.For<IShell>()
                .ImplementedBy<Shell>());

            container.Register(Component.For<BaseRepositoryDb>()
                .LifestyleTransient());

            container.Register(Component.For<FaceDetRecDB>()
                .LifestyleTransient());

            container.Register(Component.For<MainWindow>()
                .LifestyleTransient());
        }
    }
}
