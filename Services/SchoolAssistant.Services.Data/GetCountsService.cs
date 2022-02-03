namespace SchoolAssistant.Services.Data
{
    using System.Linq;

    using SchoolAssistant.Data.Common.Repositories;
    using SchoolAssistant.Data.Models;
    using SchoolAssistant.Web.ViewModels.Home;

    public class GetCountsService : IGetCountsService
    {
        private readonly IDeletableEntityRepository<Department> departmentsRepository;
        private readonly IDeletableEntityRepository<Course> coursesRepository;

        public GetCountsService(
            IDeletableEntityRepository<Department> departmentsRepository,
            IDeletableEntityRepository<Course> coursesRepository)
        {
            this.departmentsRepository = departmentsRepository;
            this.coursesRepository = coursesRepository;
        }

        public IndexViewModel GetCounts()
        {
            var data = new IndexViewModel
            {
                CoursesCount = this.coursesRepository.All().Count(),
                DepartmentsCount = this.departmentsRepository.All().Count(),
            };

            return data;
        }
    }
}
