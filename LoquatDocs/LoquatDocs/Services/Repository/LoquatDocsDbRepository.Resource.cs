using LoquatDocs.Model.Resource;

namespace LoquatDocs.Services {
  public partial class LoquatDocsDbRepository {
    private const string RESOURCE_KEY = nameof(LoquatDocsDbRepository);

    public string DatabaseNotFoundResource => _resourceProvider.GetResource(RESOURCE_KEY, "DatabaseNotFound");

    public string DatabaseNotFoundMessageResource(string dbPath) 
      => string.Format(_resourceProvider.GetResource(RESOURCE_KEY, "DatabaseNotFoundMessage"), dbPath);

    public string TemporaryDatabaseResource => _resourceProvider.GetResource(RESOURCE_KEY, "TemporaryDatabase");

    public string TemporaryDatabaseMessageResource => _resourceProvider.GetResource(RESOURCE_KEY, "TemporaryDatabaseMessage");

  }
}
