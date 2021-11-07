using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoquatDocs.Model.Resource {
  public class GeneralResources {

    private const string RESOURCE_KEY = "Resources";
    private IResourceProvider _resourceProvider;

    public GeneralResources(IResourceProvider resourceProvider) {
      _resourceProvider = resourceProvider;
    }

    public string LoquatDocs => _resourceProvider.GetResource(RESOURCE_KEY, nameof(LoquatDocs));

    public string Discard => _resourceProvider.GetResource(RESOURCE_KEY, nameof(Discard));

    public string Save => _resourceProvider.GetResource(RESOURCE_KEY, nameof(Save));

    public string Home => _resourceProvider.GetResource(RESOURCE_KEY, nameof(Home));

    public string Search => _resourceProvider.GetResource(RESOURCE_KEY, nameof(Search));

    public string List => _resourceProvider.GetResource(RESOURCE_KEY, nameof(List));

    public string Error => _resourceProvider.GetResource(RESOURCE_KEY, nameof(Error));

    public string Close => _resourceProvider.GetResource(RESOURCE_KEY, nameof(Close));

    public string Success => _resourceProvider.GetResource(RESOURCE_KEY, nameof(Success));

    public string Yes => _resourceProvider.GetResource(RESOURCE_KEY, nameof(Yes));

    public string GotIt => _resourceProvider.GetResource(RESOURCE_KEY, nameof(GotIt));

    public string DatabasePathInvalid => _resourceProvider.GetResource(RESOURCE_KEY, nameof(DatabasePathInvalid));
  }
}
