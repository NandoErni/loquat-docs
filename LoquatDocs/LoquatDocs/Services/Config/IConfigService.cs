using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace LoquatDocs.Services {
  public interface IConfigService {
    public string DatabaseFilePath { get; set; }

    Task<StorageFile> GetDatabaseFileAsync();

    Task<bool> IsDatabaseAvailable();
  }
}
