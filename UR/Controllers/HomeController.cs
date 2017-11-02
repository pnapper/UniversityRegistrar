using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using UniversityRegistrar.Models;

namespace UniversityRegistrar.Controllers
{
  public class HomeController : Controller
  {
    // HOME AND MENU ROUTES
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }
    [HttpGet("/student")]
    public ActionResult Students()
    {
      List<Student> allStudents = Student.GetAll();
      return View(allStudents);
    }
    [HttpGet("/course")]
    public ActionResult Courses()
    {
      List<Course> allCourses = Course.GetAll();
      return View(allCourses);
    }

  //NEW STUDENT
    [HttpGet("/students/new")]
    public ActionResult StudentForm()
    {
      return View();
    }

    [HttpPost("/students/new")]
    public ActionResult StudentCreate()
    {
      Student newStudent = new Student(Request.Form["student-name"], Request.Form["student-enrollment"]);
      newStudent.Save();
      return View("Success");
    }

    //NEW CATEGORY
    [HttpGet("/courses/new")]
    public ActionResult CourseForm()
    {
      return View();
    }

    [HttpPost("/courses/new")]
    public ActionResult CourseCreate()
    {
      Course newCourse = new Course(Request.Form["course-name"], Request.Form["course-number"]);
      newCourse.Save();
      return View("Success");
    }
  }
}
