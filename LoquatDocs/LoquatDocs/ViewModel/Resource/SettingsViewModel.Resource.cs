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
  }
}
