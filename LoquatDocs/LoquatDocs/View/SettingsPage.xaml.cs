using LoquatDocs.Services;
using LoquatDocs.ViewModel;
using Microsoft.UI.Xaml.Controls;
using System;

namespace LoquatDocs {
  public sealed partial class SettingsPage : Page {

    private SettingsViewModel ViewModel;

    public SettingsPage() {
      ViewModel = ServiceProvider.GetService<SettingsViewModel>();
      InitializeComponent();
      if (ViewModel.IsGettingStartedHelpNecessary()) {
        ShowGettingStartedTips();
      }
    }

    private void ShowGettingStartedTips() {
      GettingStartedTeachingTip.IsOpen = true;
      SaveBackUp.IsEnabled = false;
      CheckForUpdates.IsEnabled = false;
      GettingStartedTeachingTip.CloseButtonClick += OnGettingStartedClosed;
    }

    private void OnGettingStartedClosed(TeachingTip sender, object args) {
      CreateDatabaseTeachingTip.IsOpen = true;
      ImportDatabaseTeachingTip.IsOpen = true;
    }
  }
}
