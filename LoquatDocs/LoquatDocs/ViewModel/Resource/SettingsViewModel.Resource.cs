using LoquatDocs.Model.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoquatDocs.ViewModel {
  public partial class SettingsViewModel {

    private const string RESOURCE_KEY = "Settings";

    public string DatabaseAlreadyExistsResource => Resource.GetResource(RESOURCE_KEY, "DatabaseAlreadyExists");

    public string NoFolderChosenResource => Resource.GetResource(RESOURCE_KEY, "NoFolderChosen");

    public string ChooseFileResource => Resource.GetResource(RESOURCE_KEY, "ChooseFile");

    public string CreateNewDatabaseResource => Resource.GetResource(RESOURCE_KEY, "CreateNewDatabase");

    public string DbPathResource => Resource.GetResource(RESOURCE_KEY, "DbPath");

    public string ImportDatabaseResource => Resource.GetResource(RESOURCE_KEY, "ImportDatabase");

    public string SettingsResource => Resource.GetResource(RESOURCE_KEY, "Settings");

    public string UpdateDatabaseResource => Resource.GetResource(RESOURCE_KEY, "UpdateDatabase");

    public string SaveDatabaseBackupResource => Resource.GetResource(RESOURCE_KEY, "SaveDatabaseBackup");

    public string BackupSuccessfulResource => Resource.GetResource(RESOURCE_KEY, "BackupSuccessful");

    public string DatabaseResource => Resource.GetResource(RESOURCE_KEY, "Database");

    public string CheckDatabaseUpdatesResource => Resource.GetResource(RESOURCE_KEY, "CheckDatabaseUpdates");

    public string NoDatabaseUpdatesResource => Resource.GetResource(RESOURCE_KEY, "NoDatabaseUpdates");

    public string DatabaseUpdatesResource => Resource.GetResource(RESOURCE_KEY, "DatabaseUpdates");

    public string DatabaseUpdateSuccessfulResource => Resource.GetResource(RESOURCE_KEY, "DatabaseUpdateSuccessful");

    public string OpenLogPathResource => Resource.GetResource(RESOURCE_KEY, "OpenLogPath");
  }
}

