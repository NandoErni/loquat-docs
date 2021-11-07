using LoquatDocs.Model.Resource;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;

namespace LoquatDocs.Services {
  internal class InfoDialog {

    private ContentDialog _controlDialog;

    public InfoDialog(IResourceProvider resourceProvider, string title, string message) {
      _controlDialog = new ContentDialog() {
        Title = title,
        Content = message,
        CloseButtonText = resourceProvider.GeneralResources.Close,
        XamlRoot = App.MainWindow.Content.XamlRoot
      };
    }

    public async Task ShowAsync() {
      await _controlDialog.ShowAsync();
    }

    public static async Task CreateAndShowAsync(IResourceProvider resourceProvider, string title, string message) {
      InfoDialog dialog = new InfoDialog(resourceProvider, title, message);
      await dialog.ShowAsync();
    }

    public static async Task CreateAndShowSuccessAsync(IResourceProvider resourceProvider, string message) {
      await CreateAndShowAsync(resourceProvider, resourceProvider.GeneralResources.Success, message);
    }

    public static async Task CreateAndShowErrorAsync(IResourceProvider resourceProvider, string message) {
      await CreateAndShowAsync(resourceProvider, resourceProvider.GeneralResources.Error, message);
    }
  }
}
