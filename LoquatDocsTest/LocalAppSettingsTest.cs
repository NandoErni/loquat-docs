using LoquatDocs.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoquatDocsTest {
  [TestClass]
  public class LocalAppSettingsTest {

    private const string DATABASE_PATH = "testdb.db";
    private LocalAppSettings _localAppSettings;

    [TestInitialize]
    public void Initilize() {
      Cleanup();
    }

    [TestCleanup]
    public void Cleanup() {
      _localAppSettings = new LocalAppSettings();
      _localAppSettings.ClearAllValues();
    }

    [TestMethod]
    public void TestAddNewDatabasePath() {
      _localAppSettings.DatabaseFilePath = DATABASE_PATH;
      Assert.AreEqual(DATABASE_PATH, _localAppSettings.DatabaseFilePath);
    }

    [TestMethod]
    public void TestGetDefaultDatabasePath() {
      Assert.AreEqual(string.Empty, _localAppSettings.DatabaseFilePath);
    }

  }
}
