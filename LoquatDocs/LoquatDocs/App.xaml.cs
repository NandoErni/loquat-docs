using LoquatDocs.EntityFramework;
using LoquatDocs.Services;
using Microsoft.UI.Xaml;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LoquatDocs {

  public partial class App : Application {

    public static MainWindow MainWindow;

    public App() {
      InitializeComponent();
      UnhandledException += (s, e) => HandleUncaughtExceptions(e.Exception);
    }

    public static async void HandleUncaughtExceptions(Exception exception) {
      ServiceProvider.GetService<ILogger>()?.Error(exception.ToString());
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args) {
      StartMainWindow();
    }

    private void StartMainWindow() {
      MainWindow = new MainWindow();
      MainWindow.Activate();
    }

    public static bool QueueOnUiThread(Action action) {
      return Current.Resources.DispatcherQueue.TryEnqueue(Microsoft.System.DispatcherQueuePriority.High, action.Invoke);
    }
  }
}
