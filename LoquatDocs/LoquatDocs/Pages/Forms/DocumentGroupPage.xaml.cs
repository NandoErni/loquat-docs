using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace LoquatDocs.Pages.Forms {
  /// <summary>
  /// An empty page that can be used on its own or navigated to within a Frame.
  /// </summary>
  public sealed partial class DocumentGroupPage : Page {
    public DocumentGroupPage() {
      this.InitializeComponent();
    }

    private async void GroupAddSuggestionBoxQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args) {
      await ViewModel.SaveGroupAsync(sender.Text);
      sender.Text = string.Empty;
    }

    private async void OnItemDelete(object sender, RoutedEventArgs e) {
      await ViewModel.PromptAndDeleteGroupAsync(((Button)sender).Tag.ToString());
    }
  }
}
