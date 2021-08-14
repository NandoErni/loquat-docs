using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace LoquatDocs.Services {
  public class LocalAppSettings : ILocalAppSettings {
    public const string DATABASE_FILE_PATH_ID = "database.path";

    private ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;

    public string DatabaseFilePath { 
      get => GetSettingOrEmpty(DATABASE_FILE_PATH_ID);
      set => SetSetting(DATABASE_FILE_PATH_ID, value);
    }

    public void ClearAllValues() {
      _localSettings.Values.Clear();
    }

    private string GetSettingOrEmpty(string key) {
      if (_localSettings.Values.TryGetValue(key, out object setting)
          && setting is string) {
        return setting as string;
      }
      return string.Empty;
    }

    private void SetSetting(string key, string value) {
      _localSettings.Values[key] = value;
    }
  }
}
