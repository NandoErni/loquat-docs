using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoquatDocs.Services {
  public class DialogNotifications : INotificationService {
    public async Task<bool> NotifyDecision(string title, string message) {
      return await DecisionDialog.CreateAndShowAsync(title, message);
    }

    public async Task NotifyError(string message) {
      await InfoDialog.CreateAndShowErrorAsync(message);
    }

    public async Task NotifyInfo(string title, string message) {
      await InfoDialog.CreateAndShowAsync(title, message);
    }

    public async Task NotifySuccess(string message) {
      await InfoDialog.CreateAndShowSuccessAsync(message);
    }
  }
}
