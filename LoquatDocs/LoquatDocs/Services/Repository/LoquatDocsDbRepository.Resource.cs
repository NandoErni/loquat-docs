using LoquatDocs.Model.Resource;

namespace LoquatDocs.Services {
  public partial class LoquatDocsDbRepository {
    private const string RESOURCE_KEY = nameof(LoquatDocsDbRepository);

    public string DatabaseNotFoundResource => Resource.GetResource(RESOURCE_KEY, "DatabaseNotFound");

    public string DatabaseNotFoundMessageResource(string dbPath) 
      => string.Format(Resource.GetResource(RESOURCE_KEY, "DatabaseNotFoundMessage"), dbPath);

    public string TemporaryDatabaseResource => Resource.GetResource(RESOURCE_KEY, "TemporaryDatabase");

    public string TemporaryDatabaseMessageResource => Resource.GetResource(RESOURCE_KEY, "TemporaryDatabaseMessage");

  }
}
