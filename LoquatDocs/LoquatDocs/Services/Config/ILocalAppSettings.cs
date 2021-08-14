using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoquatDocs.Services {
  public interface ILocalAppSettings {
    string DatabaseFilePath { get; set; }
  }
}
