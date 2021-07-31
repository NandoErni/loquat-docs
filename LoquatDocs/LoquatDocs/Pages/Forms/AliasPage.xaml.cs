using Microsoft.UI.Xaml.Controls;

namespace LoquatDocs.Pages.Forms {
  public sealed partial class AliasPage : Page {
    public AliasPage() {
      this.InitializeComponent();
    }

    private async void GroupAddSuggestionBoxQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args) {
      //await ViewModel.SaveGroupAsync(sender.Text);
      sender.Text = string.Empty;
    }
  }
}
