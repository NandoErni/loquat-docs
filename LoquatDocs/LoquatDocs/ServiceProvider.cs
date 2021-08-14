using LoquatDocs.Services;
using LoquatDocs.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;

namespace LoquatDocs {
  public sealed class ServiceProvider {

    private static ServiceProvider _instance;

    private UnityContainer _container;

    private ServiceProvider() {
      _container = new UnityContainer();

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
    }

    private void RegisterViewModels() {
      _container.RegisterType<SettingsViewModel>(new ContainerControlledLifetimeManager());
      _container.RegisterType<DocumentGroupViewModel>(new ContainerControlledLifetimeManager());
      _container.RegisterType<DocumentViewModel>(new ContainerControlledLifetimeManager());
      _container.RegisterType<SearchDocumentsViewModel>(new ContainerControlledLifetimeManager());
    }
  }
}
