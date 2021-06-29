using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoquatDocs.Config {
  public interface ILocalAppSettings {
    string DatabaseFilePath { get; set; }
  }
}
