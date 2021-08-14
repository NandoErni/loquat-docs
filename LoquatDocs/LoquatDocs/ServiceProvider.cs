using LoquatDocs.Services;
using LoquatDocs.ViewModel;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;

namespace LoquatDocs {
  public sealed class ServiceProvider {

    private static ServiceProvider _instance;

    private UnityContainer _container;

    private string _logPath = 
      Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "logs", "loquatDocs.log");

    private ServiceProvider() {
      _container = new UnityContainer(); 

      Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .WriteTo.File(_logPath, rollingInterval: RollingInterval.Month)
        .CreateLogger();

      RegisterServices();
      RegisterViewModels();
    }

    private static ServiceProvider GetInstance() {
      if (_instance is null) {
        _instance = new ServiceProvider();
      }
      return _instance;
    }

    public static T GetService<T>() {
      return GetInstance()._container.Resolve<T>();
    }

    private void RegisterServices() {
      _container.RegisterType<IConfigService, Config>();
      _container.RegisterType<INotificationService, DialogNotifications>();
      _container.RegisterInstance(Log.Logger);
    }

    private void RegisterViewModels() {
      _container.RegisterType<SettingsViewModel>(new ContainerControlledLifetimeManager());
      _container.RegisterType<DocumentGroupViewModel>(new ContainerControlledLifetimeManager());
      _container.RegisterType<DocumentViewModel>(new ContainerControlledLifetimeManager());
      _container.RegisterType<SearchDocumentsViewModel>(new ContainerControlledLifetimeManager());
    }
  }
}
