using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using UniversityRegistrar.Models;

namespace UniversityRegistrar.Tests
{
  [TestClass]
  public class CourseTests : IDisposable
  {
    public CourseTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889 ;database=ur_test;";
    }

    [TestMethod]
    public void GetAll_CoursesEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Course.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueForSameName_Course()
    {
      //Arrange, Act
      Course firstCourse = new Course("Trad-Harm 3", "Music 101");
      Course secondCourse = new Course("Trad-Harm 3", "Music 101");

      //Assert
      Assert.AreEqual(firstCourse, secondCourse);
    }

    [TestMethod]
      public void Save_SavesCourseToDatabase_CourseList()
      {
        //Arrange
        Course testCourse = new Course("Trad-Harm 3", "Music 101");
        testCourse.Save();

        //Act
        List<Course> result = Course.GetAll();
        List<Course> testList = new List<Course>{testCourse};

        //Assert
        CollectionAssert.AreEqual(testList, result);
      }

      [TestMethod]
      public void Save_DatabaseAssignsIdToCourse_Id()
      {
        //Arrange
        Course testCourse = new Course("Trad-Harm 3", "Music 101");
        testCourse.Save();

        //Act
        Course savedCourse = Course.GetAll()[0];

        int result = savedCourse.GetId();
        int testId = testCourse.GetId();

        //Assert
        Assert.AreEqual(testId, result);
      }

      [TestMethod]
      public void Find_FindsCourseInDatabase_Course()
      {
        //Arrange
        Course testCourse = new Course("Trad-Harm 3", "Music 101");
        testCourse.Save();

        //Act
        Course foundCourse = Course.Find(testCourse.GetId());

        //Assert
        Assert.AreEqual(testCourse, foundCourse);
      }

    public void Dispose()
    {
      Student.DeleteAll();
      Course.DeleteAll();
    }
  }
}
