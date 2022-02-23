namespace SchoolAssistant.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SchoolAssistant.Common;
    using SchoolAssistant.Data.Models;
    using SchoolAssistant.Services.Data;
    using SchoolAssistant.Web.ViewModels.Courses;
    using SchoolAssistant.Web.ViewModels.Lectures;

    public class CoursesController : BaseController
    {
        private readonly IDepartmentsService departmentsService;
        private readonly ICoursesService coursesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public CoursesController(
            IDepartmentsService departmentsService,
            ICoursesService coursesService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.departmentsService = departmentsService;
            this.coursesService = coursesService;
            this.userManager = userManager;
            this.environment = environment;
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
            try
            {
                await this.coursesService.CreateAsync(input, user.Id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
            }

            // Redirect to course info page
            return this.Redirect("/");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var inputModel = this.coursesService.GetById<EditCourseInputModel>(id);

            if (this.User.Identity.Name == inputModel.CreatedByUserUserName || this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                inputModel.Id = id;
                inputModel.CreatedByUserUserName = this.User.Identity.Name;
                inputModel.DepartmentsItems = this.departmentsService.GetAllAsKeyValuePairs();
                return this.View(inputModel);
            }

            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditCourseInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Id = id;
                input.CreatedByUserUserName = this.User.Identity.Name;
                input.DepartmentsItems = this.departmentsService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            await this.coursesService.UpdateAsync(id, input);
            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        [Authorize]
        public IActionResult AddLecture(int id)
        {
            var inputModel = this.coursesService.GetById<CreateLectureInputModel>(id);

            if (this.User.Identity.Name == inputModel.CreatedByUserUserName || this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                inputModel.Id = id;
                inputModel.CreatedByUserUserName = this.User.Identity.Name;
                return this.View(inputModel);
            }

            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddLecture(int id, CreateLectureInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Id = id;
                input.CreatedByUserUserName = this.User.Identity.Name;
                return this.View(input);
            }

            await this.coursesService.AddLectureAsync(id, input, $"{this.environment.WebRootPath}/presentations");
            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        public IActionResult All(int id = 1)
        {
            const int ItemsPerPage = 12;
            var viewModel = new CoursesListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                CoursesCount = this.coursesService.GetCount(),
                Courses = this.coursesService.GetAll<CourseInListViewModel>(id, 12),
            };

            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var course = this.coursesService.GetById<SingleCourseViewModel>(id);
            return this.View(course);
        }

        public IActionResult LecturesById(int id)
        {
            var lecture = this.coursesService.GetLectureById<SingleLectureViewModel>(id);
            return this.View(lecture);
        }
    }
}
