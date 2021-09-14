using LoquatDocs.Services;
using LoquatDocs.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoquatDocs.Test {
  [TestClass]
  public class DocumentGroupViewModelTest {
    private DocumentGroupViewModel _viewModel;

    [TestInitialize]
    public void Initilize() {
      Mock<INotificationService> notificationServiceMock = new Mock<INotificationService>();
      notificationServiceMock.Setup(x => x.NotifyDecision(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.Run(() => true));

      _viewModel = new DocumentGroupViewModel(notificationServiceMock.Object, UnitTestHelper.GetRepositoryMock(UnitTestHelper.DefaultGroups).Object);
    }

    [TestMethod]
    public async Task TestInitilizeGroupNames() {
      Assert.IsTrue(!_viewModel.Groups.Any());
      await _viewModel.InitilizeDocumentGroups();

      Assert.IsTrue(_viewModel.Groups.Any());
      Assert.AreEqual(_viewModel.Groups.Count, UnitTestHelper.DefaultGroups.Count);
    }
  }
}
