using MySql.Data.MySqlClient;
using school.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Http;
using System.Diagnostics;
using System.Web.Http.Cors;
using System.Runtime.Remoting.Messaging;

namespace school.Controllers
{
    public class TeacherDataController : ApiController
    {
        private SchoolDBContext School = new SchoolDBContext();

        ///<summary>
        ///This Controller will access to the teacher table of school database
        ///return a list of teachers in the system with teachername 
        /// </summary>
        /// <example1>GET api/TeacherData/ListTeachers</example>
        /// <return>A List of teachers </return>
        /// <example1>GET api/TeacherData/ListTeachers/Linda</example>
        /// <return><EmployeeNum>T382</EmployeeNum>
        /// <Hiredate>2015-08-22T00:00:00</Hiredate>
        /// <Salary>60.22</Salary>
        /// <TeacherFname>Linda</TeacherFname>
        /// <TeacherId>3</TeacherId>
        /// <TeacherLname>Chan</TeacherLname>
        /// </return>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{TeacherSearchKey?}")]
        public List<Teacher> ListTeachers(string TeacherSearchKey = null)
        {
            //Step 1.Creat an instance of a connection

            MySqlConnection conn = School.AccessDatabase();

            //Step 2.Open the connection between the web server and database

            conn.Open();

            //Step 3.Establish a new command  for our database

            MySqlCommand cmd = conn.CreateCommand();

            //Step 4.SQL QUERY

            cmd.CommandText = "Select * from Teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";

            //Step 5.excute the sql command


            cmd.Parameters.AddWithValue("@key", "%" + TeacherSearchKey + "%");
            cmd.Prepare();

            //Step 6.Gather Result Set of Query in to a variable

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Step 7. Create an empty list of Teachers

            List<Teacher> Teachers = new List<Teacher>();

            //Step 8.Loop through each row the result set

            while (ResultSet.Read())
            {
                //Step 9.Access column info by database column name as an index

                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNum = ResultSet["employeenumber"].ToString();
                DateTime Hiredate = (DateTime)ResultSet["hiredate"];
                decimal Salary = Convert.ToDecimal(ResultSet["salary"]);

                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNum = EmployeeNum;
                NewTeacher.Hiredate = Hiredate;
                NewTeacher.Salary = Salary;

                //Step 10.Add teacher Name to the list
                Teachers.Add(NewTeacher);
            }
            //Step 11.Close the connection
            conn.Close();

            return Teachers;
        }

        ///<summary>
        ///return an individual teacher from databaae by specifying the primary key teacherid
        /// </summary>
        /// <param name="id">the teacherid</param>
        ///  <example>GET api/TeacherData/FindTeacher/8</example>
        /// <return><EmployeeNum>T401</EmployeeNum>
        /// <Hiredate>2014-06-26T00:00:00</Hiredate>
        /// <Salary>71.15</Salary>
        /// <TeacherFname>Dana</TeacherFname>
        /// <TeacherId>8</TeacherId>
        /// <TeacherLname>Ford</TeacherLname></return>

        [HttpGet]
        public Teacher FindTeacher(int id)
        {

            Teacher NewTeacher = new Teacher();
            //Step 1.Creat an instance of a connection

            MySqlConnection conn = School.AccessDatabase();

            //Step 2.Open the connection between the web server and database

            conn.Open();

            //Step 3.Establish a new command  for our database

            MySqlCommand cmd = conn.CreateCommand();

            //Step 4.SQL QUERY

            string query = "select t.teacherid,t.teacherfname ,t.teacherlname,t.employeenumber,t.hiredate,t.salary FROM teachers t  WHERE t.teacherid=@id ";

            //Step 5.excute the sql command

            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            //Step 6.Gather Result Set of Query in to a variable

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Step 7.Loop through each row the result set

            while (ResultSet.Read())
            {
                //Step 8.Access column info by database column name as an index

                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNum = ResultSet["employeenumber"].ToString();
                DateTime Hiredate = (DateTime)ResultSet["hiredate"];
                decimal Salary = Convert.ToDecimal(ResultSet["salary"]);

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.Courses = Courses(id);
                NewTeacher.EmployeeNum = EmployeeNum;
                NewTeacher.Hiredate = Hiredate;
                NewTeacher.Salary = Salary;
            }
            //Step 9.Close the connection
            conn.Close();


            return NewTeacher;
        }
        ///<summary>
        ///return classes by teacherid
        /// </summary>
        /// <param name="id">the teacherid</param>
        ///  <example>GET api/TeacherData/Courses/8</example>
        /// <return><string>Database Development</string>
        /// <string>Web Information Architecture</string>
        /// </return>

        [HttpGet]
        public List<string> Courses(int id)
        {
            //Step 1.Creat an instance of a connection

            MySqlConnection conn = School.AccessDatabase();

            //Step 2.Open the connection between the web server and database

            conn.Open();

            //Step 3.Establish a new command  for our database

            MySqlCommand cmd = conn.CreateCommand();

            //Step 4.SQL QUERY

            cmd.CommandText = "Select classname from classes where teacherid=@id";

            //Step 5.excute the sql command


            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            //Step 6.Gather Result Set of Query in to a variable

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Step 7. Create an empty list of Courses

            List<string> Courses = new List<string>();

            //Step 8.Loop through each row the result set

            while (ResultSet.Read())
            {
                //Step 9.Access column info by database column name as an index


                string ClassName = ResultSet["classname"].ToString();




                //Step 10.Add Class Name to the list
                Courses.Add(ClassName);
            }
            //Step 11.Close the connection
            conn.Close();

            return Courses;
        }




        /// <summary>
        /// This method receives a new teacher's info and insert it into the database.
        /// </summary>
        /// <param name="AddedTeacher">A teacher object</param>
        /// <returns></returns>
        /// <example>Post api/TeacherData/</example>
        [HttpPost]
        [Route("api/TeacherData/AddTeacher")]
        public string AddTeacher([FromBody] Teacher AddedTeacher)
        {    ///check form element TeacherFname at least has one character, and not max out length of 255
            if (string.IsNullOrWhiteSpace(AddedTeacher.TeacherFname) || System.Text.RegularExpressions.Regex.IsMatch(AddedTeacher.TeacherFname, @"^\w+$") == false || AddedTeacher.TeacherFname.Length > 255)
            { return "error"; }
            ///check form element TeacherLname at least has one character, and not max out length of 255
            else if (string.IsNullOrWhiteSpace(AddedTeacher.TeacherLname) || System.Text.RegularExpressions.Regex.IsMatch(AddedTeacher.TeacherLname, @"^\w+$") == false || AddedTeacher.TeacherLname.Length > 255)
            { return "error"; }
            ///check form element EmployeeNum at least has one character, and not max out length of 255
            else if (string.IsNullOrWhiteSpace(AddedTeacher.EmployeeNum) || System.Text.RegularExpressions.Regex.IsMatch(AddedTeacher.EmployeeNum, @"^\w+$") == false || AddedTeacher.EmployeeNum.Length > 255)
            { return "error"; }
            ///check form element Hiredate at least has one character

            else if (string.IsNullOrWhiteSpace(AddedTeacher.Hiredate.ToString()))
            { return "error"; }
            ///check form element Salary at least has one character
            else if (string.IsNullOrWhiteSpace(AddedTeacher.Salary.ToString()))
            { return "error"; }
            else
            {
                MySqlConnection Conn = School.AccessDatabase();
                Conn.Open();
                MySqlCommand cmd = Conn.CreateCommand();
                cmd.CommandText = "INSERT INTO teachers(teacherfname,teacherlname,employeenumber,hiredate,salary) VALUES(@teacherfname,@teacherlname,@employeenumber,@hiredate,@salary);";

                cmd.Parameters.AddWithValue("@teacherfname", AddedTeacher.TeacherFname);
                cmd.Parameters.AddWithValue("@teacherlname", AddedTeacher.TeacherLname);
                cmd.Parameters.AddWithValue("@employeenumber", AddedTeacher.EmployeeNum);
                cmd.Parameters.AddWithValue("@hiredate", AddedTeacher.Hiredate);
                cmd.Parameters.AddWithValue("@salary", AddedTeacher.Salary);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                return "sucess";
            }
        }


        /// <summary>
        /// Deletes an Teacher from the connected MySQL Database if the ID of that teacher exists. Does NOT maintain relational integrity. Non-Deterministic.
        /// </summary>
        /// <param name="id">The ID of the teacher.</param>
        /// <example>POST /api/TeacherData/DeleteTeacher/3</example>
        [HttpPost]
        [Route("api/TeacherData/DeleteTeacher/{teacherId}")]
        public void DeleteTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Delete from teachers WHERE teacherid=@id ";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();


        }
        /// <summary>
        /// Update an Teacher from the connected MySQL Database 
        /// </summary>
        /// <param name="id">The ID of the teacher.</param>
        /// <param name="UpdatedTeacher">"UpdatedTeacher"</param>
        /// <example>POST /api/TeacherData/UpdateTeacher/{id}</example>
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"TeacherFname":"Liz",
        ///	"TeacherLname":"Song",
        ///	"EmployeeNum":"T414",
        ///	"Hiredate":"2024-01-01 00:00:00",
        ///	"Salary":"100",
        /// }
        [HttpPost]
        [Route("api/TeacherData/UpdateTeacher/{TeacherId}")]
        [EnableCors(origins:"*", methods:"*",headers:"*")]
        public string UpdateTeacher(int TeacherId, [FromBody]Teacher UpdatedTeacher)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            Debug.WriteLine(UpdatedTeacher.TeacherFname);

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            string query = "UPDATE teachers SET teacherfname=@TeacherFname, teacherlname=@TeacherLname, employeenumber=@EmployeeNum, hiredate=@Hiredate, salary=@Salary WHERE teacherid=@TeacherId";
            cmd.CommandText = query;

            cmd.Parameters.AddWithValue("@TeacherId", TeacherId);
            cmd.Parameters.AddWithValue("@TeacherFname", UpdatedTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", UpdatedTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@EmployeeNum", UpdatedTeacher.EmployeeNum);
            cmd.Parameters.AddWithValue("Hiredate", UpdatedTeacher.Hiredate);
            cmd.Parameters.AddWithValue("@Salary", UpdatedTeacher.Salary);


            cmd.Prepare();
            cmd.ExecuteNonQuery();

            Conn.Close();

            return "sucess";
        }
        
    }
}

