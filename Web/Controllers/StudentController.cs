using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Web.Models;

namespace Web.Controllers
{
    public class StudentController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> Index(User user)
        {
            var result = await Business.ApiClientManager.GetAllStudents(user);
            List<StudentViewModel> students = new List<StudentViewModel>();
            StudentViewModel student = new StudentViewModel();
            
            foreach (var item in result)
            {
                student.Id = item.Id;
                student.FirstName = item.FirstName;
                student.LastName = item.LastName;
                student.Dob = item.Dob;
                student.Sex = item.Sex;
                students.Add(student);
            }

            return await Task.Run(() => View(students));

        }
    }
}