namespace SchoolAssistant.Web.ViewModels.Courses
{
    using SchoolAssistant.Data.Models;
    using SchoolAssistant.Services.Mapping;

    public class CourseInListViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string DepartmentName { get; set; }

        public string Description { get; set; }

        public string CreatedByUserUserName { get; set; }
    }
}
