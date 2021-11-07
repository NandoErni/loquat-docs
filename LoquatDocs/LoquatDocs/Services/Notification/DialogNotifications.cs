using LoquatDocs.Model.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoquatDocs.Services {
  public class DialogNotifications : INotificationService {
    IResourceProvider _resourceProvider;

    public DialogNotifications(IResourceProvider resourceProvider) {
      _resourceProvider = resourceProvider;
    }
    public async Task<bool> NotifyDecision(string title, string message, string confirmMessage = "") {
      return await DecisionDialog.CreateAndShowAsync(_resourceProvider, title, message, confirmMessage);
    }

    public async Task NotifyError(string message) {
      await InfoDialog.CreateAndShowErrorAsync(_resourceProvider, message);
    }

    public async Task NotifyInfo(string title, string message) {
      await InfoDialog.CreateAndShowAsync(_resourceProvider, title, message);
    }

    public async Task NotifySuccess(string message) {
      await InfoDialog.CreateAndShowSuccessAsync(_resourceProvider, message);
    }
  }
}
