using LoquatDocs.Model;
using LoquatDocs.Model.Resource;
using LoquatDocs.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace LoquatDocs.ViewModel {
  public partial class SettingsViewModel : ObservableObject {

    public const string DB_FILE_ENDING = ".loquatdb";

    public const string DEFAULT_DB_NAME = "database" + DB_FILE_ENDING;

    public string DEFAULT_DB_BACKUP_NAME => "backup\\" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + DEFAULT_DB_NAME;

    private IConfigService _config;

    private INotificationService _notification;

    private ILogger _logger;

    private IProcessHelperService _processHelper;

    private ILoquatDocsDbRepository _repository;

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

    public IAsyncRelayCommand CheckUpdateDatabaseCommand { get; }

    public IAsyncRelayCommand SaveDatabaseBackupCommand { get; }

    public IAsyncRelayCommand OpenLogPathCommand { get; }

    public SettingsViewModel(IConfigService config, INotificationService notificationService, ILogger logger, 
      IProcessHelperService processHelper, ILoquatDocsDbRepository repository) {
      _logger = logger;
      _config = config;
      _notification = notificationService;
      _processHelper = processHelper;
      _repository = repository;
      CreateNewDatabaseCommand = new AsyncRelayCommand(CreateNewDatabase);
      ImportDatabaseCommand = new AsyncRelayCommand(PickDbFile);
      CheckUpdateDatabaseCommand = new AsyncRelayCommand(CheckUpdateDatabase);
      SaveDatabaseBackupCommand = new AsyncRelayCommand(SaveDatabaseBackup);
      OpenLogPathCommand = new AsyncRelayCommand(OpenLogPath);
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
        return;
      }
      string databaseFilePath = Path.Combine(databaseFolder.Path, DEFAULT_DB_NAME);

      if (DoesDbAlreadyExist(databaseFilePath)) {
        await _notification.NotifyError(DatabaseAlreadyExistsResource);
        return;
      }

      await _repository.CreateOrUpdateDatabaseAsync(databaseFilePath);

      StorageFile db = await databaseFolder.GetFileAsync(DEFAULT_DB_NAME);
      DbPath = db != null ? db.Path : (DbPath ?? string.Empty);
    }

    public async Task CheckUpdateDatabase() {
      bool hasUpdates = await _repository.AnyDatabaseUpdates() || true;

      var dialog = new ContentDialog() {
        Title = UpdateDatabaseResource,
        Content = hasUpdates ? DatabaseUpdatesResource : NoDatabaseUpdatesResource,
        CloseButtonText = GeneralResources.Close,
        XamlRoot = App.MainWindow.Content.XamlRoot
      };

      if (hasUpdates) {
        dialog.PrimaryButtonText = UpdateDatabaseResource;
        dialog.PrimaryButtonClick += async (s, a) => await UpdateDatabase();
      }

      await dialog.ShowAsync();
    }

    public async Task UpdateDatabase() {
      if (string.IsNullOrWhiteSpace(DbPath)) {
        await PickDbFile();
        if (string.IsNullOrWhiteSpace(DbPath)) {
          return;
        }
      }

      await _repository.CreateOrUpdateDatabaseAsync(DbPath);
      App.QueueOnUiThread(async () => await _notification.NotifyInfo(UpdateDatabaseResource, DatabaseUpdateSuccessfulResource));
    }

    public async Task SaveDatabaseBackup() {
      string savePath = Path.Combine(Path.GetDirectoryName(DbPath), DEFAULT_DB_BACKUP_NAME);
      try {
        if (!Directory.Exists(Path.GetDirectoryName(savePath))) {
          Directory.CreateDirectory(Path.GetDirectoryName(savePath));
        }
        File.Copy(DbPath, savePath);
      } catch (Exception e) {
        await _notification.NotifyError(ErrorDatabaseBackupResource);
        _logger.Error(e.ToString());
        return;
      }

      await _notification.NotifyInfo(DatabaseResource, BackupSuccessfulResource);

      _processHelper.OpenFileLocationInExplorer(savePath);
    }

    public async Task OpenLogPath() {
      _processHelper.OpenFolderInExplorer(Path.GetDirectoryName(ServiceProvider.LogPath));
    }

    private bool DoesDbAlreadyExist(string pickedDbPath) {
      FileInfo pickedDbInfo = new FileInfo(pickedDbPath);
      return pickedDbInfo.Exists;
    }

    public bool IsGettingStartedHelpNecessary() {
      if (String.IsNullOrWhiteSpace(_config.DatabaseFilePath)) {
        return true;
      }
      return false;
    }
  }
}
