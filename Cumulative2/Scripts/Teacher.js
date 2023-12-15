function UpdateTeacher(TeacherId) {

    var TeacherFname = document.getElementById("TeacherFname").value;
    var TeacherLname = document.getElementById("TeacherLname").value;
    var EmployeeNum = document.getElementById("EmployeeNum").value;
    var Hiredate = document.getElementById("Hiredate").value;
    var Salary = document.getElementById("Salary").value;

    if (TeacherFname === "") {
        return "error";

    }
    else if (TeacherLname === "") {
        return "error";

    }
    else if (EmployeeNum === "") {
        return "error";

    }
    else if (Hiredate === "") {
        return "error";

    }
    else if (Salary === "") {
        return "error";

    }
    else {


        var TeacherData = {
            "TeacherFname": TeacherFname,
            "TeacherLname": TeacherLname,
            "EmployeeNum": EmployeeNum,
            "Hiredate": Hiredate,
            "Salary": Salary
        };


        var url = "http://localhost:51508/api/TeacherData/UpdateTeacher/" + TeacherId;

        var rq = new XMLHttpRequest();
        rq.open("POST", url, true);
        rq.setRequestHeader("Content-Type", "application/json");
        
        rq.onreadystatechange = function () {
            if (rq.readyState == 4) {
                if (rq.status == 200) {
                    alert("sucess!");
                }
                else {
                    alert("error");
                }
            }
        }

        rq.send(JSON.stringify(TeacherData));
    }
}