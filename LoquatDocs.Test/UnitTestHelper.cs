using LoquatDocs.EntityFramework.Model;
using LoquatDocs.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoquatDocs.Test {
  public class UnitTestHelper {

    public static List<Group> DefaultGroups;

    static UnitTestHelper() {
      DefaultGroups = new List<Group>();
      DefaultGroups.Add(new Group() {
        Groupname = "Yobama"
      });
    }

    public static Mock<ILoquatDocsDbRepository> GetRepositoryMock(List<Group> groups) {
      var repositoryMock = new Mock<ILoquatDocsDbRepository>();
      repositoryMock.Setup(m => m.GetAllGroupnames()).Returns(Task.Run(() => groups.Select(g => g.Groupname).ToList()));

      return repositoryMock;
    }
  }
}
