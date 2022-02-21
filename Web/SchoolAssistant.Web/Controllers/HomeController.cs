namespace SchoolAssistant.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using SchoolAssistant.Services.Data;
    using SchoolAssistant.Web.ViewModels;
    using SchoolAssistant.Web.ViewModels.Courses;
    using SchoolAssistant.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly IDepartmentsService departmentsService;
        private readonly ICoursesService coursesService;

        public HomeController(
            IDepartmentsService departmentsService,
            ICoursesService coursesService)
        {
            this.departmentsService = departmentsService;
            this.coursesService = coursesService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                CoursesCount = this.coursesService.GetCount(),
                DepartmentsCount = this.departmentsService.GetCount(),
                RandomCourses = this.coursesService.GetRandom<CourseInListViewModel>(10),
            };

            return this.View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
