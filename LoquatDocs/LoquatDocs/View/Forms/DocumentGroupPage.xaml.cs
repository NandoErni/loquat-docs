using LoquatDocs.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace LoquatDocs.View.Forms {
  public sealed partial class DocumentGroupPage : Page {

    private DocumentGroupViewModel ViewModel;

    public DocumentGroupPage() {
      ViewModel = ServiceProvider.GetService<DocumentGroupViewModel>();
      this.InitializeComponent();
    }

    private async void GroupAddSuggestionBoxQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args) {
      await ViewModel.SaveGroupAsync(sender.Text);
      sender.Text = string.Empty;
    }

    private async void OnItemDelete(object sender, RoutedEventArgs e) {
      await ViewModel.PromptAndDeleteGroupAsync(((Button)sender).Tag.ToString());
    }

    private async void OnPageLoaded(object sender, RoutedEventArgs e) {
      await ViewModel.InitilizeDocumentGroups();
    }
  }
}
