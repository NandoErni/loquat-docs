using System;

namespace LoquatDocs.EntityFramework {
  public class InvalidDatabasePathException : ArgumentException {
    public InvalidDatabasePathException(string message) : base(message) {

    }
  }
}
