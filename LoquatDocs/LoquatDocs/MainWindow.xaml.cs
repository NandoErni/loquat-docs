using LoquatDocs.EntityFramework;
using LoquatDocs.Pages;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
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

namespace LoquatDocs {
  /// <summary>
  /// An empty window that can be used on its own or navigated to within a Frame.
  /// </summary>
  public sealed partial class MainWindow : Window {
    private readonly List<(string Tag, Type PageType)> _pages = new List<(string Tag, Type Page)>
    {
        ("home", typeof(HomePage)),
        ("list", typeof(ListDocumentsPage)),
        ("search", typeof(SearchPage)),
    };

    public MainWindow() {

      this.InitializeComponent();
    }

    private void myButton_Click(object sender, RoutedEventArgs e) {
      using (LoquatDocsDbContext ctx = new LoquatDocsDbContext(null, true)) {
        ctx.Documents.Add(new EntityFramework.Model.Document() { Path = "Lol", Title = "Yeea" });
        ctx.SaveChanges();
      }
    }

    private void OnNavigationViewItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args) {
      if (args.InvokedItemContainer.IsSelected) {
        return;
      }
      if (args.IsSettingsInvoked) {
        OnSettingsButtonClicked();
        return;
      }
      var navItemTag = args.InvokedItemContainer.Tag.ToString();
      Navigate(navItemTag, args.RecommendedNavigationTransitionInfo);
    }

    private void OnSettingsButtonClicked() {
      if (ContentFrame.CurrentSourcePageType != typeof(SettingsPage)) {
        ContentFrame.Navigate(typeof(SettingsPage));
      }
    }

    private void Navigate(string navItemTag, NavigationTransitionInfo transitionInfo) {
      Type page = GetPageType(navItemTag);

      if (page is not null) {
        ContentFrame.Navigate(page, null, transitionInfo);
      }
    }

    private Type GetPageType(string tag) {
      return _pages.FirstOrDefault(p => p.Tag.Equals(tag)).PageType;
    }
  }
}
