using LoquatDocs.Model.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoquatDocs.ViewModel {
  public partial class SettingsViewModel {

    private const string RESOURCE_KEY = "Settings";

    public string DatabaseAlreadyExistsResource => _resourceProvider.GetResource(RESOURCE_KEY, "DatabaseAlreadyExists");

    public string ChooseFileResource => _resourceProvider.GetResource(RESOURCE_KEY, "ChooseFile");

    public string CreateNewDatabaseResource => _resourceProvider.GetResource(RESOURCE_KEY, "CreateNewDatabase");

    public string DbPathResource => _resourceProvider.GetResource(RESOURCE_KEY, "DbPath");

    public string ImportDatabaseResource => _resourceProvider.GetResource(RESOURCE_KEY, "ImportDatabase");

    public string SettingsResource => _resourceProvider.GetResource(RESOURCE_KEY, "Settings");

    public string UpdateDatabaseResource => _resourceProvider.GetResource(RESOURCE_KEY, "UpdateDatabase");

    public string SaveDatabaseBackupResource => _resourceProvider.GetResource(RESOURCE_KEY, "SaveDatabaseBackup");

    public string BackupSuccessfulResource => _resourceProvider.GetResource(RESOURCE_KEY, "BackupSuccessful");

    public string DatabaseResource => _resourceProvider.GetResource(RESOURCE_KEY, "Database");

    public string CheckDatabaseUpdatesResource => _resourceProvider.GetResource(RESOURCE_KEY, "CheckDatabaseUpdates");

    public string NoDatabaseUpdatesResource => _resourceProvider.GetResource(RESOURCE_KEY, "NoDatabaseUpdates");

    public string DatabaseUpdatesResource => _resourceProvider.GetResource(RESOURCE_KEY, "DatabaseUpdates");

    public string DatabaseUpdateSuccessfulResource => _resourceProvider.GetResource(RESOURCE_KEY, "DatabaseUpdateSuccessful");

    public string OpenLogPathResource => _resourceProvider.GetResource(RESOURCE_KEY, "OpenLogPath");

    public string GettingStartedResource => _resourceProvider.GetResource(RESOURCE_KEY, "GettingStarted");

    public string GettingStartedTeachingTipHintResource => _resourceProvider.GetResource(RESOURCE_KEY, "GettingStartedTeachingTipHint");

    public string CreateNewDatabaseTeachingTipHintResource => _resourceProvider.GetResource(RESOURCE_KEY, "CreateNewDatabaseTeachingTipHint");

    public string ImportExistingDatabaseResource => _resourceProvider.GetResource(RESOURCE_KEY, "ImportExistingDatabase");

    public string ImportExistingDatabaseTeachingTipHintResource => _resourceProvider.GetResource(RESOURCE_KEY, "ImportExistingDatabaseTeachingTipHint");

    public string ErrorDatabaseBackupResource => _resourceProvider.GetResource(RESOURCE_KEY, "ErrorDatabaseBackup");

    public string GotItResource => _resourceProvider.GeneralResources.GotIt;
  }
}

