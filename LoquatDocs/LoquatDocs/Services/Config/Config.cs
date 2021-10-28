using LoquatDocs.ViewModel;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace LoquatDocs.Services {
  public class Config : IConfigService {

    private ILocalAppSettings _localAppSettings = new LocalAppSettings();

    public Config(ILocalAppSettings localAppSettings) {
      _localAppSettings = localAppSettings;
    }

    public Config() : this(new LocalAppSettings()) { }

    public string DatabaseFilePath {
      get => _localAppSettings.DatabaseFilePath; 
      set => _localAppSettings.DatabaseFilePath = value; 
    }

    public async Task<StorageFile> GetDatabaseFileAsync() {
      try {
        return await StorageFile.GetFileFromPathAsync(DatabaseFilePath);
      } catch (FileNotFoundException) {
        return null;
      }
    }

    public async Task<bool> IsDatabaseAvailable() {
      StorageFile db = await GetDatabaseFileAsync();
      return db is not null && db.IsAvailable;
    }

    public string GetTemporaryDatabasePath() {
      return Path.Combine(Path.GetTempPath(), SettingsViewModel.DEFAULT_DB_NAME);
    }

    public bool IsDatabasePathTemporary() {
      return Path.GetTempPath()
        .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) == Directory.GetParent(DatabaseFilePath).FullName;
    }
  }
}
