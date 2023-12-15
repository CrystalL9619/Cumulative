using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Google.Protobuf.WellKnownTypes;
using school.Models;
using System.Diagnostics;


using HttpPostAttribute = System.Web.Http.HttpPostAttribute;


namespace school.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }
        //GET: /Teacher/List? TeacherSearchKey = { value }
        /// <summary>
        /// This Controller will return a list of teachers or a individual teacher in the system with teachername
        /// </summary>
        /// <param name="TeacherSearchKey"></param>
        /// <example>/Teacher/List</example>
        /// <returns>a list of teachers</returns>
        /// <example>/Teacher/List?TeacherSearchKey={value}</example>
        /// <returns>a teacher whose name contains {value}</returns>
        public ActionResult List(string TeacherSearchKey)
        {
            TeacherDataController controller = new TeacherDataController();
            List<Teacher> Teachers = controller.ListTeachers(TeacherSearchKey);

            return View(Teachers);
        }
        //GET: /Teacher/Show/{id}
        /// <summary>
        /// This Controller will return an individual teacher in the system with Class(es) he/she teachs
        /// </summary>
        /// <param name="id"></param>
        /// <example>/Teacher/Show/{id}</example>
        /// <returns>a teacher's name whose teacher id is {id} with the class(es) he/she teachs</returns>
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
            

            return View(NewTeacher);
        }
        

        //Views/Teacher/New.cshtml
        public ActionResult New()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(string TeacherFname, string TeacherLname, string EmployeeNum, DateTime HireDate, decimal Salary)
        {
            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.EmployeeNum = EmployeeNum;
            NewTeacher.Hiredate = HireDate;
            NewTeacher.Salary = Salary;

            TeacherDataController Controller = new TeacherDataController();
            Controller.AddTeacher(NewTeacher);
            return RedirectToAction("List");
        }
     
        //GET : /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);


            return View(NewTeacher);
        }


        //POST : /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }
        //GET: Teacher/Edit/{id}
        //route to /Views/Teacher/Edit.cshtml
      
        public ActionResult Edit(int id)
        {
          TeacherDataController controller = new TeacherDataController();
          Teacher Selectedteacher = controller.FindTeacher(id);  
            return View(Selectedteacher);
        }

        public ActionResult Ajex_Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher Selectedteacher = controller.FindTeacher(id);

            return View(Selectedteacher);
        }

        /// <summary>
        /// Receives a POST request containing information about an existing teacher in the system, with new values. Conveys this information to the API, and redirects to the "Teache/Show/{TeacherId}" page of our updated Teacher.
        /// </summary>
        /// <param name="id">Id of the Teacher to update</param>
        /// <param name="TeacherFname">The updated first name of the Teacher</param>
        /// <param name="TeacherLname">The updated last name of the Teacher</param>
        /// <param name="EmployeeNum">The updated employee number of the Teacher.</param>
        /// <param name="Hiredate">The updated hire date of the Teacher.</param>
        /// <param name="Salary">The updated Salary of the Teacher.</param>
        /// <returns>A dynamic webpage which provides the current information of the Teacher.</returns>
        /// <example>
        /// POST : /Teacher/Update/
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"TeacherFname":"Liz",
        ///	"TeacherLname":"Song",
        ///	"EmployeeNum":"T414",
        ///	"Hiredate":"2024-01-01 00:00:00",
        ///	"Salary":"100"
        /// }
        /// </example>


        //POST:/Teacher/Update/{id}
        [HttpPost]
        public ActionResult Update(int id,string TeacherFname, string TeacherLname, string EmployeeNum, DateTime Hiredate, decimal Salary)
        {
           
            Teacher UpdatedTeacher = new Teacher();
            UpdatedTeacher.TeacherFname = TeacherFname;
            UpdatedTeacher.TeacherLname = TeacherLname;
            UpdatedTeacher.EmployeeNum = EmployeeNum;   
            UpdatedTeacher.Hiredate = Hiredate;
            UpdatedTeacher.Salary = Salary;
            TeacherDataController controller= new TeacherDataController();
                controller.UpdateTeacher(id, UpdatedTeacher);
            return RedirectToAction("Show/"+id);
        }
    }
}