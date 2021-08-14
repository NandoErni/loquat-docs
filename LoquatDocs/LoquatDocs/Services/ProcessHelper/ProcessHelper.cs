using System;
using System.Diagnostics;

namespace LoquatDocs.Services {
  public class ProcessHelper : IProcessHelperService {
    public void OpenFileInExplorer(string filePath) {
      Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
    }

    public void OpenFileLocationInExplorer(string filePath) {
      Process.Start("explorer.exe", "/select, \"" + filePath + "\"");
    }

    public void OpenFolderInExplorer(string folderPath) {
      Process.Start("explorer.exe", $"\"{folderPath}\"");
    }
  }
}
