using LoquatDocs.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using System.Threading.Tasks;

namespace LoquatDocsTest {
  [TestClass]
  public class ConfigTest {
    private const string DATABASE_FILE_PATH = "\\test.db";

    private Config config;

    [TestInitialize]
    public void Initilize() {
      Mock<ILocalAppSettings> mockedSettings = CreateMockedLocalAppSettings();

      config = new Config(mockedSettings.Object);
    }

    [TestCleanup]
    public void Cleanup() {

    }

    private Mock<ILocalAppSettings> CreateMockedLocalAppSettings() {
      Mock<ILocalAppSettings> mockedSettings = new Mock<ILocalAppSettings>();

      mockedSettings.SetupProperty(mock => mock.DatabaseFilePath, DATABASE_FILE_PATH);

      return mockedSettings;
    }
  }
}
