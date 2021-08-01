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
  }
}
