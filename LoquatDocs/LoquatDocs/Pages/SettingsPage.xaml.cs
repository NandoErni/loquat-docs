using LoquatDocs.EntityFramework;
using LoquatDocs.Model;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.IO;
using System;
using Windows.Storage;
using System.Threading.Tasks;
using LoquatDocs.ViewModel;

namespace LoquatDocs {
  public sealed partial class SettingsPage : Page {

    public SettingsPage() {
      InitializeComponent();
    }

    private async void OnPickDbFile(object sender, RoutedEventArgs e) {
      await ViewModel.PickDbFile();
    }

    private async void OnCreateNewDatabase(object sender, RoutedEventArgs e) {
      await ViewModel.CreateNewDatabase();
    }
  }
}
