using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace UniversityRegistrar.Models
{
  public class Course
  {
    private string _courseName;
    private string _courseNumber;
    private int _id;
    public Course(string courseName, string courseNumber, int id = 0)
    {
      _courseName = courseName;
      _courseNumber = courseNumber;
      _id = id;
    }

    public override bool Equals(System.Object otherCourse)
    {
      if (!(otherCourse is Course))
      {
        return false;
      }
      else
      {
        Course newCourse = (Course) otherCourse;
        return this.GetId().Equals(newCourse.GetId());
      }
    }

    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }

    public string GetCourseName()
    {
      return _courseName;
    }

    public string GetCourseNumber()
    {
      return _courseNumber;
    }

    public int GetId()
    {
      return _id;
    }

    public static List<Course> GetAll()
    {
      List<Course> allCourses = new List<Course> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM course;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int courseId = rdr.GetInt32(0);
        string courseName = rdr.GetString(1);
        string courseNumber = rdr.GetString(2);
        Course newCourse = new Course(courseName, courseNumber, courseId);
        allCourses.Add(newCourse);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCourses;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO course (name, course_number) VALUES (@courseName, @courseNumber);";

      MySqlParameter courseName = new MySqlParameter();
      courseName.ParameterName = "@courseName";
      courseName.Value = this._courseName;
      cmd.Parameters.Add(courseName);

      MySqlParameter courseNumber = new MySqlParameter();
      courseNumber.ParameterName = "@courseNumber";
      courseNumber.Value = this._courseNumber;
      cmd.Parameters.Add(courseNumber);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Course Find(int id)
  {
    MySqlConnection conn = DB.Connection();
    conn.Open();
    var cmd = conn.CreateCommand() as MySqlCommand;
    cmd.CommandText = @"SELECT * FROM course WHERE id = (@searchId);";

    MySqlParameter searchId = new MySqlParameter();
    searchId.ParameterName = "@searchId";
    searchId.Value = id;
    cmd.Parameters.Add(searchId);

    var rdr = cmd.ExecuteReader() as MySqlDataReader;
    int CourseId = 0;
    string CourseName = "";
    string CourseNumber = "";

    while(rdr.Read())
    {
      CourseId = rdr.GetInt32(0);
      CourseName = rdr.GetString(1);
      CourseNumber = rdr.GetString(2);
    }
    Course newCourse = new Course(CourseName, CourseNumber, CourseId);
    conn.Close();
    if (conn != null)
    {
      conn.Dispose();
    }
    return newCourse;
  }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM course;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
