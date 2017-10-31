using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniversityRegistrar.Models;
using System.Collections.Generic;
using System;

namespace UniversityRegistrar.Tests
{

  [TestClass]
  // public class TaskTests : IDisposable
  public class StudentTests : IDisposable
  {
    public StudentTests()
    {
        DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=ur_test;";
    }

    public void Dispose()
    {
      Student.DeleteAll();
      Course.DeleteAll();
    }

    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Student.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_OverrideTrueIfNamesAreTheSame_Student()
    {
      // Arrange, Act
      Student firstStudent = new Student("Joe Blow", "2017-01-01");
      Student secondStudent = new Student("Joe Blow", "2017-01-01");

      // Assert
      Assert.AreEqual(firstStudent, secondStudent);
    }

    [TestMethod]
    public void Save_SavesToDatabase_StudentList()
    {
      //Arrange
      Student testStudent = new Student("Joe Blow", "2017-01-01", 1);

      //Act
      testStudent.Save();
      List<Student> result = Student.GetAll();
      List<Student> testList = new List<Student>{testStudent};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
  }
}
