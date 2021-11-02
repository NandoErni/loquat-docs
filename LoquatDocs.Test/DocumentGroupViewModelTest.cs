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
    private UnitTestHelper _unitTestHelper;
    private DocumentGroupViewModel _viewModel;
    private Mock<ILoquatDocsDbRepository> _repository;

    [TestInitialize]
    public void Initilize() {
      _unitTestHelper = new UnitTestHelper();
      Mock<INotificationService> notificationServiceMock = new Mock<INotificationService>();
      notificationServiceMock.Setup(x => x.NotifyDecision(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.Run(() => true));
      _repository = _unitTestHelper.GetRepositoryMock();
      _viewModel = new DocumentGroupViewModel(notificationServiceMock.Object, _repository.Object);
    }

    [TestMethod]
    public async Task TestInitilizeGroupNames() {
      Assert.IsFalse(_viewModel.Groups.Any());
      await _viewModel.InitilizeDocumentGroups();

      Assert.IsTrue(_viewModel.Groups.Any());
      Assert.AreEqual(_viewModel.Groups.Count, _unitTestHelper.DefaultGroups.Count);
    }

    [TestMethod]
    public async Task TestSaveGroupAsync() {
      const string groupName = "Yobumianio";
      Assert.IsFalse(_viewModel.Groups.Any());

      await _viewModel.SaveGroupAsync(groupName);

      Assert.AreEqual(_viewModel.Groups.Count, 1);
      Assert.AreEqual(_viewModel.Groups.FirstOrDefault(), groupName);

      Assert.AreEqual((await _repository.Object.GetAllGroupnames()).Count, _unitTestHelper.DefaultGroups.Count);
      Assert.IsTrue((await _repository.Object.GetAllGroupnames()).Any(g => g is groupName));
    }

    [TestMethod]
    public async Task TestPromptAndDeleteGroupAsync() {
      await _viewModel.PromptAndDeleteGroupAsync("Yobama");

      _repository.Verify(x => x.DeleteGroup("Yobama"), Times.Once);
    }
  }
}
