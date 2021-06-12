using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LoquatDocs {
  /// <summary>
  /// An empty page that can be used on its own or navigated to within a Frame.
  /// </summary>
  public sealed partial class MainPage : Page {
    public MainPage() {
      this.InitializeComponent();
    }
    private async void SaveButton_Click(object sender, RoutedEventArgs e) {
      Windows.Storage.StorageFolder storageFolder = await PickFolder();
      Windows.Storage.StorageFile sampleFile = await storageFolder.CreateFileAsync("sample.txt",
        Windows.Storage.CreationCollisionOption.ReplaceExisting);
      Thread.Sleep(5000);
      await sampleFile.DeleteAsync();
      Windows.Storage.ApplicationData.Current.LocalSettings.Values["config.path"] = "D:\\Projects\\Visual Studio\\LoquatDocs\\README.md";
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e) {
      string localSettings = Windows.Storage.ApplicationData.Current.LocalSettings.Values["config.path"] as string;
    }

    private void Button_Click_2(object sender, RoutedEventArgs e) {

    }

    private async Task<Windows.Storage.StorageFolder> PickFolder() {
      // PoC:
      
      var picker = new Windows.Storage.Pickers.FolderPicker();
      picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
      picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
      picker.FileTypeFilter.Add(".jpg");
      picker.FileTypeFilter.Add(".jpeg");
      picker.FileTypeFilter.Add(".png");

      Windows.Storage.StorageFolder folder = await picker.PickSingleFolderAsync();
      return folder;
    }
  }
}
