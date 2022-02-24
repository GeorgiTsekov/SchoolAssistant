namespace SchoolAssistant.Web.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using SchoolAssistant.Services.Data;
    using SchoolAssistant.Web.ViewModels.Courses;
    using SchoolAssistant.Web.ViewModels.SearchCourses;

    public class SearchCoursesController : BaseController
    {
        private readonly ICoursesService coursesService;

        public SearchCoursesController(ICoursesService coursesService)
        {
            this.coursesService = coursesService;
        }

        [HttpGet]
        public IActionResult Index(string name)
        {
            var viewModel = new LecturesNameViewModel
            {
                Name = name,
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult List(string name)
        {
            var viewModel = new ListViewModel
            {
                Courses = this.coursesService.GetCoursesByLectureName<CourseInListViewModel>(name),
            };

            return this.View(viewModel);
        }
    }
}
