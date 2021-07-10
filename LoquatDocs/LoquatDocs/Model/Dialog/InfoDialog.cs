using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;

namespace LoquatDocs.Model.Dialog {
  public class InfoDialog {

    private ContentDialog _controlDialog;

    public InfoDialog(string title, string message) {
      _controlDialog = new ContentDialog() {
        Title = title,
        Content = message,
        CloseButtonText = "Close",
        XamlRoot = App.Window.Content.XamlRoot
      };
    }

    public async Task ShowAsync() {
      await _controlDialog.ShowAsync();
    }

    public static async Task CreateAndShowAsync(string title, string message) {
      InfoDialog dialog = new InfoDialog(title, message);
      await dialog.ShowAsync();
    }

    public static async Task CreateAndShowSuccessAsync(string message) {
      await CreateAndShowAsync("Success", message);
    }

    public static async Task CreateAndShowErrorAsync(string message) {
      await CreateAndShowAsync("Error", message);
    }
  }
}
