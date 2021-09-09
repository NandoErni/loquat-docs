using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace LoquatDocs.Services {
  public class StandardFolderPicker : StandardFileSystemPicker {
    private FolderPicker _folderPicker;

    public StandardFolderPicker() {
      _folderPicker = new FolderPicker();
      _folderPicker.FileTypeFilter.Add("*");
      _folderPicker.ViewMode = PickerViewMode.Thumbnail;
      _folderPicker.SuggestedStartLocation = PickerLocationId.ComputerFolder;
    }

    public async Task<StorageFolder> PickSingleFolderAsync() {
      SetWindowHandler(_folderPicker);
      return await _folderPicker.PickSingleFolderAsync();
    }
  }
}
