using LoquatDocs.Model;
using LoquatDocs.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
namespace LoquatDocs.Pages {

  public sealed partial class SearchPage : Page {

    private SearchDocumentsViewModel ViewModel;

    public SearchPage() {
      ViewModel = ServiceProvider.GetService<SearchDocumentsViewModel>();
      InitializeComponent();
    }

    private async void OnOpenFile(object sender, RoutedEventArgs e) {
      await ViewModel.OpenFile(((Button)sender).Tag as string);
    }

    private async void OnOpenFileLocation(object sender, RoutedEventArgs e) {
      await ViewModel.OpenFileLocation(((Button)sender).Tag as string);
    }

    private async void OnEditDocument(object sender, RoutedEventArgs e) {
      await ViewModel.EditDocument(((Button)sender).Tag as string);
    }

    private async void OnDeleteDocument(object sender, RoutedEventArgs e) {
      await ViewModel.SafeDeleteDocument(((Button)sender).Tag as string);
    }

    private void OnSearch(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args) {
      ViewModel.Search(args.QueryText.Trim(), GetSearchArguments());
    }

    private SearchArguments GetSearchArguments() {
      SearchArguments arguments = SearchArguments.None;

      if (Title.IsChecked.Value) arguments |= SearchArguments.Title;
      if (Tags.IsChecked.Value) arguments |= SearchArguments.Tags;
      if (GroupName.IsChecked.Value) arguments |= SearchArguments.GroupName;
      if (FilePath.IsChecked.Value) arguments |= SearchArguments.FilePath;
      if (DocumentDate.IsChecked.Value) arguments |= SearchArguments.DocumentDate;
      if (DocumentDueDate.IsChecked.Value) arguments |= SearchArguments.DocumentDueDate;

      return arguments;
    }

    private void OnToggleSearchFilter(object sender, RoutedEventArgs e) {
      Filter.Visibility = ((ToggleButton)sender).IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
    }

    private void OnPageLoaded(object sender, RoutedEventArgs e) {
      ViewModel.InitilizeList();
      ProgressRing.Visibility = Visibility.Collapsed;
    }

    private async void OnInvoicePayed(object sender, RoutedEventArgs e) {
      await ViewModel.PayInvoice(((Button)sender).Tag as string);
    }
  }
}
