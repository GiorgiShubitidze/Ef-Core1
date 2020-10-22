using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ef_core.Models;
using System.Net.NetworkInformation;

namespace Ef_core.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
     
        public IActionResult Index()
        {

            using (var db = new StudentDbContext())
            {
                //Studen.Name StudenGrades.Point 

                List<View> StudentForView = new List<View>();

                foreach (var item in db.StudentGrades.ToList())
                {
                    var NewStudent = new View();
                    NewStudent.ID = item.Id;
                    NewStudent.Point = item.Point;
                    StudentForView.Add(NewStudent);
                }
                foreach (var std in StudentForView)
                {
                    var result = db.Students.Where(x => x.StudentId == std.ID).Select(x => x.Name);
                    std.Name = result.FirstOrDefault();
                }

                return View(StudentForView);
                // var student = db.Students.Where(x => x.StudentId == studentGrades.StudentId).FirstOrDefault();
            }

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
