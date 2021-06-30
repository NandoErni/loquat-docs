using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using WinRT;

namespace LoquatDocs.Model {
  public class StandardFilePicker : StandardFileSystemPicker {
    private FileOpenPicker _filePicker;

    public static StandardFilePicker UniversalFilePicker => new StandardFilePicker(new List<string>() { "*" });
    public static StandardFilePicker DatabaseFilePicker => new StandardFilePicker(new List<string>() { ".loquatdb" });

    public StandardFilePicker(List<string> fileTypeFilter) {
      _filePicker = new FileOpenPicker();
      _filePicker.ViewMode = PickerViewMode.Thumbnail;
      _filePicker.SuggestedStartLocation = PickerLocationId.ComputerFolder;

      fileTypeFilter.ForEach(filter => _filePicker.FileTypeFilter.Add(filter));
    }

    public async Task<StorageFile> PickDatabaseFileAsync() {
      SetWindowHandler(_filePicker);
      return await _filePicker.PickSingleFileAsync();
    }
  }

    
}
