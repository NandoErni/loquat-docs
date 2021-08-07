using Microsoft.ApplicationModel.Resources;

namespace LoquatDocs.Model.Resource {
  public class Resource {

    private static ResourceManager _resourceManager = new ResourceManager();

    public static string GetResource(string resourceType, string resourceId) {
      return GetSubTree(resourceType).GetValue(resourceId).ValueAsString;
    }

    public static string GetFormattedResource(string resourceType, string resourceId, params object[] args) {
      return string.Format(GetResource(resourceType, resourceId), args);
    }

    private static ResourceMap GetSubTree(string resourceType) {
      return _resourceManager.MainResourceMap.GetSubtree(resourceType);
    }

  }
}
