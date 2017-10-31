using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace UniversityRegistrar.Models
{
  public class Course
  {
    private string _name;
    private string _number;
    private int _id;
    public Course(string name, string number, int id = 0)
    {
      _name = name;
      _number = number;
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

    public string GetName()
    {
      return _name;
    }

    public string GetNumber()
    {
      return _number;
    }

    public int GetId()
    {
      return _id;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO course (name, number) VALUES (@name, @coursenum);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      MySqlParameter number = new MySqlParameter();
      name.ParameterName = "@coursenum";
      name.Value = this._number;
      cmd.Parameters.Add(number);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

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
