using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace LoquatDocs.Pages.Forms {
  public sealed partial class DocumentPage : Page {
    public DocumentPage() {
      this.InitializeComponent();
    }

    private void GroupNameAutoSuggestBoxTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args) {
      if (args.Reason.Equals(AutoSuggestionBoxTextChangeReason.UserInput)) {
        ViewModel.UpdateFilteredGroupNameSuggestions();
      }
    }

    private void TagAutoSuggestBoxTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args) {
      if (args.Reason.Equals(AutoSuggestionBoxTextChangeReason.UserInput)) {
        ViewModel.UpdateFilteredTagSuggestionList(sender.Text);
      }
    }

    private void TagAutoSuggestionBoxQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args) {
      if (args.ChosenSuggestion != null) {
        ViewModel.AddTagToList(args.ChosenSuggestion.ToString());
      } else {
        ViewModel.AddTagToList(args.QueryText);
      }
      sender.Text = string.Empty;
    }

    private void OnItemDelete(object sender, RoutedEventArgs e) {
      ViewModel.Tags.Remove(((Button)sender).Tag.ToString());
    }
  }
}
