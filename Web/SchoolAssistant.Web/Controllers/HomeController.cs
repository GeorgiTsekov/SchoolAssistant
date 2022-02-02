namespace SchoolAssistant.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using SchoolAssistant.Data;
    using SchoolAssistant.Web.ViewModels;
    using SchoolAssistant.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly ApplicationDbContext db;

        public HomeController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                CoursesCount = this.db.Courses.Count(),
                TeachersCount = 0,
                DepartmentsCount = this.db.Department.Count(),
                StudentsCount = 0,
            };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
