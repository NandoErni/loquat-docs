using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
      return await StorageFile.GetFileFromPathAsync(DatabaseFilePath);
    }

    public async Task<bool> IsDatabaseAvailable() {
      return (await GetDatabaseFileAsync()).IsAvailable;
    }
  }
}
