using LoquatDocs.ViewModel;
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
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace LoquatDocs.Pages {
  /// <summary>
  /// An empty page that can be used on its own or navigated to within a Frame.
  /// </summary>
  public sealed partial class SearchPage : Page {
    public SearchPage() {
      this.InitializeComponent();
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

    private async void OnSearch(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args) {
      await ViewModel.Search(args.QueryText.Trim(), GetSearchArguments());
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

    private async void OnPageLoaded(object sender, RoutedEventArgs e) {
      await ViewModel.InitilizeList();
      ProgressRing.Visibility = Visibility.Collapsed;
    }
  }
}
