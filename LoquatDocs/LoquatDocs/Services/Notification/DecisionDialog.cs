using LoquatDocs.Model.Resource;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;

namespace LoquatDocs.Services {
  internal class DecisionDialog {

    private ContentDialog _controlDialog;

    public DecisionDialog(string title, string message, string confirmMessage = "") {
      _controlDialog = new ContentDialog() {
        Title = title,
        Content = message,
        CloseButtonText = GeneralResources.Close,
        PrimaryButtonText = string.IsNullOrWhiteSpace(confirmMessage) ? GeneralResources.Yes : confirmMessage,
        XamlRoot = App.MainWindow.Content.XamlRoot
      };
    }

    private async Task<bool> ShowAsync() {
      ContentDialogResult result = await _controlDialog.ShowAsync();
      return result == ContentDialogResult.Primary;
    }

    public static async Task<bool> CreateAndShowAsync(string title, string errorMessage, string confirmMessage = "") {
      DecisionDialog dialog = new DecisionDialog(title, errorMessage, confirmMessage);
      return await dialog.ShowAsync();
    }
  }
}
