using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoquatDocs.Model.Resource {
  public interface IResourceProvider {

    public GeneralResources GeneralResources { get; }

    public string GetResource(string resourceType, string resourceId);

    public string GetFormattedResource(string resourceType, string resourceId, params object[] args);
  }
}
