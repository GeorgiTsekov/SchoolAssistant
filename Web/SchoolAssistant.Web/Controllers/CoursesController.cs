namespace SchoolAssistant.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SchoolAssistant.Data.Models;
    using SchoolAssistant.Services.Data;
    using SchoolAssistant.Web.ViewModels.Courses;

    public class CoursesController : Controller
    {
        private readonly IDepartmentsService departmentsService;
        private readonly ICoursesService coursesService;
        private readonly UserManager<ApplicationUser> userManager;

        public CoursesController(
            IDepartmentsService departmentsService,
            ICoursesService coursesService,
            UserManager<ApplicationUser> userManager)
        {
            this.departmentsService = departmentsService;
            this.coursesService = coursesService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateCourseInputModel
            {
                DepartmentsItems = this.departmentsService.GetAllAsKeyValuePairs(),
            };
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.DepartmentsItems = this.departmentsService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            //// var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var user = await this.userManager.GetUserAsync(this.User);
            await this.coursesService.CreateAsync(input, user.Id);

            // Redirect to course info page
            return this.Redirect("/");
        }

        public IActionResult All(int id = 1)
        {
            var viewModel = new CoursesListViewModel
            {
                PageNumber = id,
                Courses = this.coursesService.GetAll(id, 12),
            };

            return this.View(viewModel);
        }
    }
}
