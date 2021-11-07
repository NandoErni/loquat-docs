using Microsoft.ApplicationModel.Resources;

namespace LoquatDocs.Model.Resource {
  public class Resource : IResourceProvider {

    private ResourceManager _resourceManager = new ResourceManager();

    public GeneralResources GeneralResources { get; private set; }

    public Resource() {
      GeneralResources = new GeneralResources(this);
    }

    public string GetResource(string resourceType, string resourceId) {
      return GetSubTree(resourceType).GetValue(resourceId).ValueAsString;
    }

    public string GetFormattedResource(string resourceType, string resourceId, params object[] args) {
      return string.Format(GetResource(resourceType, resourceId), args);
    }

    private ResourceMap GetSubTree(string resourceType) {
      return _resourceManager.MainResourceMap.GetSubtree(resourceType);
    }

  }
}
