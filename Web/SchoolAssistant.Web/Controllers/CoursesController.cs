namespace SchoolAssistant.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SchoolAssistant.Services.Data;
    using SchoolAssistant.Web.ViewModels.Courses;

    public class CoursesController : Controller
    {
        private readonly IDepartmentsService departmentsService;
        private readonly ICoursesService coursesService;

        public CoursesController(
            IDepartmentsService departmentsService,
            ICoursesService coursesService)
        {
            this.departmentsService = departmentsService;
            this.coursesService = coursesService;
        }

        public IActionResult Create()
        {
            var viewModel = new CreateCourseInputModel
            {
                DepartmentsItems = this.departmentsService.GetAllAsKeyValuePairs(),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.DepartmentsItems = this.departmentsService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            await this.coursesService.CreateAsync(input);

            // Redirect to course info page
            return this.Redirect("/");
        }
    }
}
