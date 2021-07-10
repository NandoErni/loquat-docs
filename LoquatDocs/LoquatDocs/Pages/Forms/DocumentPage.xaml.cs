using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace LoquatDocs.Pages.Forms {
  /// <summary>
  /// An empty page that can be used on its own or navigated to within a Frame.
  /// </summary>
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
  }
}
