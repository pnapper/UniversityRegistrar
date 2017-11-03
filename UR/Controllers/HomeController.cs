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

    //ONE STUDENT
    [HttpGet("/students/{id}")]
    public ActionResult StudentDetail(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Student selectedStudent = Student.Find(id);
      List<Course> StudentCourses = selectedStudent.GetCourses();
      List<Course> AllCourses = Course.GetAll();
      model.Add("student", selectedStudent);
      model.Add("studentCourses", StudentCourses);
      model.Add("allCourses", AllCourses);
      return View("StudentDetail", model);

    }

    //ONE COURSE
    [HttpGet("/courses/{id}")]
    public ActionResult CourseDetail(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Course SelectedCourse = Course.Find(id);
      List<Student> CourseStudents = SelectedCourse.GetStudents();
      List<Student> AllStudents = Student.GetAll();
      model.Add("course", SelectedCourse);
      model.Add("courseStudents", CourseStudents);
      model.Add("allStudents", AllStudents);
      return View(model);
    }

    //ADD STUDENT TO COURSE
    [HttpPost("courses/{courseId}/students/new")]
    public ActionResult CourseAddStudent(int courseId)
    {
      Course course = Course.Find(courseId);
      Student student = Student.Find(Int32.Parse(Request.Form["student-id"]));
      course.AddStudent(student);
      return View("Success");
    }
    //ADD CATEGORY TO TASK
    [HttpPost("students/{studentId}/courses/new")]
    public ActionResult StudentAddCourse(int studentId)
    {
      Student student = Student.Find(studentId);
      Course course = Course.Find(Int32.Parse(Request.Form["course-id"]));
      student.AddCourse(course);
      return View("Success");
    }
  }
}
