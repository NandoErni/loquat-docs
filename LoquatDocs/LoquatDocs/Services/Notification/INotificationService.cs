using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoquatDocs.Services {
  public interface INotificationService {
    public Task NotifyInfo(string title, string message);

    public Task NotifySuccess(string message);

    public Task NotifyError(string message);

    public Task<bool> NotifyDecision(string title, string message, string confirmText = "");
  }
}
