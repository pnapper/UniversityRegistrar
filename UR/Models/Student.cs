using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace UniversityRegistrar.Models
{
  public class Student
  {
    private int _id;
    private string _name;
    private string _enrollment;

    public Student(string name, string enrollment, int Id = 0)
    {
      _id = Id;
      _name = name;
      _enrollment = enrollment;
    }

    public override bool Equals(System.Object otherStudent)
    {
      if (!(otherStudent is Student))
      {
        return false;
      }
      else
      {
        Student newStudent = (Student) otherStudent;
        bool idEquality = (this.GetId() == newStudent.GetId());
        bool nameEquality = (this.GetName() == newStudent.GetName());
        bool enrollmentEquality = (this.GetEnrollment() == newStudent.GetEnrollment());
        return (idEquality && nameEquality && enrollmentEquality);
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

    public int GetId()
    {
      return _id;
    }

    public string GetEnrollment()
    {
      return _enrollment;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO student (name, enrollment) VALUES (@name, @enrollment);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      MySqlParameter enrollment = new MySqlParameter();
      enrollment.ParameterName = "@enrollment";
      enrollment.Value = this._enrollment;
      cmd.Parameters.Add(enrollment);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Student> GetAll()
    {
      List<Student> allStudents = new List<Student> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM student;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int studentId = rdr.GetInt32(0);
        string studentName = rdr.GetString(1);
        string studentEnrollment = rdr.GetString(2);
        Student newStudent = new Student(studentName, studentEnrollment, studentId);
        allStudents.Add(newStudent);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allStudents;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM student;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
