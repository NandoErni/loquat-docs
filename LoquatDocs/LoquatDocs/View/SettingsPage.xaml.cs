using LoquatDocs.ViewModel;
using Microsoft.UI.Xaml.Controls;

namespace LoquatDocs {
  public sealed partial class SettingsPage : Page {

    private SettingsViewModel ViewModel;

    public SettingsPage() {
      ViewModel = ServiceProvider.GetService<SettingsViewModel>();
      InitializeComponent();
    }
  }
}
