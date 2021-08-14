namespace LoquatDocs.Services {
  public interface IProcessHelperService {

    void OpenFileInExplorer(string filePath);

    void OpenFileLocationInExplorer(string filePath);

    void OpenFolderInExplorer(string folderPath);
  }
}
