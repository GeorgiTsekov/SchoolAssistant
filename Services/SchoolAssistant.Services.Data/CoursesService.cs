namespace SchoolAssistant.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using SchoolAssistant.Data.Common.Repositories;
    using SchoolAssistant.Data.Models;
    using SchoolAssistant.Web.ViewModels.Courses;

    public class CoursesService : ICoursesService
    {
        private readonly IDeletableEntityRepository<Course> courseRepository;

        public CoursesService(
            IDeletableEntityRepository<Course> courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public async Task CreateAsync(CreateCourseInputModel input, string userId)
        {
            var course = new Course
            {
                Name = input.Name,
                Description = input.Description,
                DepartmentId = input.DepartmentId,
                CreatedByUserId = userId,
            };

            await this.courseRepository.AddAsync(course);
            await this.courseRepository.SaveChangesAsync();
        }

        public IEnumerable<CourseInListViewModel> GetAll(int page, int itemsPerPage = 12)
        {
            var courses = this.courseRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new CourseInListViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    DepartmentName = x.Department.Name,
                    DepartmentId = x.DepartmentId,
                    Description = x.Description,
                    CreatedByUserName = x.CreatedByUser.UserName,
                })
                .ToList();

            return courses;
        }

        public int GetCount()
        {
            return this.courseRepository.All().Count();
        }
    }
}
