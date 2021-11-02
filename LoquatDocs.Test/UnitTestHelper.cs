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

    public List<Group> DefaultGroups;

   public UnitTestHelper(bool seedData = true) {
      DefaultGroups = new List<Group>();

      if (seedData) {
        SeedData();
      }
    }

    public void ResetData() {
      DefaultGroups.Clear();
    }

    private void SeedData() {
      DefaultGroups.Add(new Group() {
        Groupname = "Yobama"
      });
    }

    public Mock<ILoquatDocsDbRepository> GetRepositoryMock() {
      var repositoryMock = new Mock<ILoquatDocsDbRepository>();
      repositoryMock.Setup(m => m.GetAllGroupnames()).Returns(() => Task.Run(() => DefaultGroups.Select(g => g.Groupname).ToList()));
      repositoryMock.Setup(m => m.SaveGroup(It.IsAny<string>())).Returns<string>(str => Task.Run(() => DefaultGroups.Add(new Group() { Groupname = str})));

      return repositoryMock;
    }
  }
}
