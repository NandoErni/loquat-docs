using LoquatDocs.Pages.Forms;
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
  public sealed partial class HomePage : Page {

    private readonly List<(string Tag, Type PageType)> _pages = new List<(string Tag, Type Page)>
    {
        (nameof(Document), typeof(DocumentPage)),
        (nameof(DocumentGroup), typeof(DocumentGroupPage)),
        (nameof(Alias), typeof(AliasPage)),
        //(nameof(AliasGroup), typeof(AliasGroupPage)),
    };

    public HomePage() {
      this.InitializeComponent();
      Navigate(nameof(Document));
    }

    private void OnNavigationItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args) {
      if (args.InvokedItemContainer.IsSelected) {
        return;
      }
      var navigationItemName = args.InvokedItemContainer.Name.ToString();
      Navigate(navigationItemName);
    }

    private void Navigate(string navigationItemName) {
      Type page = GetPageType(navigationItemName);

      if (page is not null) {
        ContentFrame.Navigate(page);
      }
    }

    private Type GetPageType(string name) {
      return _pages.FirstOrDefault(p => p.Tag.Equals(name)).PageType;
    }
  }
}
