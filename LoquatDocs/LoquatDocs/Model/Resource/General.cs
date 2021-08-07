using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoquatDocs.Model.Resource {
  public static class General {

    private const string RESOURCE_KEY = "Resources";

    public static string LoquatDocs => Resource.GetResource(RESOURCE_KEY, nameof(LoquatDocs));

    public static string Discard => Resource.GetResource(RESOURCE_KEY, nameof(Discard));

    public static string Save => Resource.GetResource(RESOURCE_KEY, nameof(Save));

    public static string Home => Resource.GetResource(RESOURCE_KEY, nameof(Home));

    public static string Search => Resource.GetResource(RESOURCE_KEY, nameof(Search));

    public static string List => Resource.GetResource(RESOURCE_KEY, nameof(List));
  }
}
