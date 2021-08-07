using LoquatDocs.Model;
using LoquatDocs.Model.Dialog;
using LoquatDocs.Model.Resource;
using LoquatDocs.ViewModel.Repository;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace LoquatDocs.ViewModel {
  public partial class SettingsViewModel : ObservableObject {

    public const string DB_FILE_ENDING = ".loquatdb";

    public const string DEFAULT_DB_NAME = "database" + DB_FILE_ENDING;

    public string DEFAULT_DB_BACKUP_NAME = "backup\\" + DateTime.Now.ToString("yyyyMMddTHHmmss") + DEFAULT_DB_NAME;

    private Config.Config _config = new Config.Config();

    private LoquatDocsDbRepository _repository = new LoquatDocsDbRepository();

    private Settings _settings = new Settings();

    public string DbPath {
      get => _settings.DbPath;
      set { 
        SetProperty(ref _settings.DbPath, value);
        _config.DatabaseFilePath = value;
      }
    }

    public IAsyncRelayCommand CreateNewDatabaseCommand { get; }

    public IAsyncRelayCommand ImportDatabaseCommand { get; }

    public IAsyncRelayCommand UpdateDatabaseCommand { get; }

    public IAsyncRelayCommand SaveDatabaseBackupCommand { get; }

    public SettingsViewModel() {
      CreateNewDatabaseCommand = new AsyncRelayCommand(CreateNewDatabase);
      ImportDatabaseCommand = new AsyncRelayCommand(PickDbFile);
      UpdateDatabaseCommand = new AsyncRelayCommand(UpdateDatabase);
      SaveDatabaseBackupCommand = new AsyncRelayCommand(SaveDatabaseBackup);
      DbPath = _config.DatabaseFilePath;
    }

    public async Task PickDbFile() {
      StandardFilePicker filePicker = StandardFilePicker.DatabaseFilePicker;

      StorageFile db = await filePicker.PickSingleFileAsync();
      DbPath = db != null ? db.Path : string.Empty;
    }

    public async Task CreateNewDatabase() {
      StandardFolderPicker picker = new StandardFolderPicker();

      StorageFolder databaseFolder = await picker.PickSingleFolderAsync();

      if (databaseFolder is null) {
        await InfoDialog.CreateAndShowErrorAsync(NoFolderChosenResource);
        return;
      }
      string databaseFilePath = Path.Combine(databaseFolder.Path, DEFAULT_DB_NAME);

      if (DoesDbAlreadyExist(databaseFilePath)) {
        await InfoDialog.CreateAndShowErrorAsync(DatabaseAlreadyExistsResource);
        return;
      }

      await _repository.CreateOrUpdateDatabaseAsync(databaseFilePath);

      StorageFile db = await databaseFolder.GetFileAsync(DEFAULT_DB_NAME);
      DbPath = db != null ? db.Path : string.Empty;
    }

    public async Task UpdateDatabase() {
      if (string.IsNullOrWhiteSpace(DbPath)) {
        await PickDbFile();
        if (string.IsNullOrWhiteSpace(DbPath)) {
          return;
        }
      }

      await _repository.CreateOrUpdateDatabaseAsync(DbPath);
    }

    public async Task SaveDatabaseBackup() {
      string savePath = Path.Combine(Path.GetDirectoryName(DbPath), DEFAULT_DB_BACKUP_NAME);
      try {
        if (!Directory.Exists(Path.GetDirectoryName(savePath))) {
          Directory.CreateDirectory(Path.GetDirectoryName(savePath));
        }
        File.Copy(DbPath, savePath);
      } catch (Exception e) {

        return;
      }

      var dialog = new InfoDialog(DatabaseResource, BackupSuccessfulResource);
      await dialog.ShowAsync();

      Process.Start("explorer.exe", "/select, \"" + savePath + "\"");
    }

    private bool DoesDbAlreadyExist(string pickedDbPath) {
      FileInfo pickedDbInfo = new FileInfo(pickedDbPath);
      return pickedDbInfo.Exists;
    }
  }
}
