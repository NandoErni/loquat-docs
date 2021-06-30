using LoquatDocs.EntityFramework;
using LoquatDocs.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace LoquatDocs.ViewModel {
  public class SettingsViewModel : ObservableObject {

    public const string DB_FILE_ENDING = ".loquatdb";

    public const string DEFAULT_DB_NAME = "database" + DB_FILE_ENDING;

    private Settings _settings = new Settings();

    public string DbPath {
      get => _settings.DbPath;
      set => SetProperty(ref _settings.DbPath, value);
    }

    public IAsyncRelayCommand CreateNewDatabaseCommand { get; }

    public IAsyncRelayCommand ImportDatabaseCommand { get; }

    public SettingsViewModel() {
      CreateNewDatabaseCommand = new AsyncRelayCommand(CreateNewDatabase);
      ImportDatabaseCommand = new AsyncRelayCommand(PickDbFile);
    }

    public async Task PickDbFile() {
      StandardFilePicker filePicker = StandardFilePicker.DatabaseFilePicker;

      StorageFile db = await filePicker.PickDatabaseFileAsync();
      DbPath = db != null ? db.Path : string.Empty;
    }

    public async Task CreateNewDatabase() {
      StandardFolderPicker picker = new StandardFolderPicker();

      StorageFolder databaseFolder = await picker.PickSingleFolderAsync();
      string databaseFilePath = Path.Combine(databaseFolder.Path, DEFAULT_DB_NAME);

      if (DoesDbAlreadyExist(databaseFilePath)) {
        return;
      }

      using LoquatDocsDbContext context = new LoquatDocsDbContext(databaseFilePath, true);

      StorageFile db = await databaseFolder.GetFileAsync(DEFAULT_DB_NAME);
      DbPath = db != null ? db.Path : string.Empty;
    }

    private bool DoesDbAlreadyExist(string pickedDbPath) {
      FileInfo pickedDbInfo = new FileInfo(pickedDbPath);
      return pickedDbInfo.Exists;
    }
  }
}
