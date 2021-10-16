using LoquatDocs.View.Forms;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LoquatDocs.View {

  public sealed partial class HomePage : Page {

    private readonly List<(string Tag, Type PageType)> _pages = new List<(string Tag, Type Page)>
    {
        (nameof(Document), typeof(DocumentPage)),
        (nameof(DocumentGroup), typeof(DocumentGroupPage)),
        //(nameof(Alias), typeof(AliasPage)),
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
