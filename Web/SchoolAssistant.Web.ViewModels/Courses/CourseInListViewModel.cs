namespace SchoolAssistant.Web.ViewModels.Courses
{
    using AutoMapper;
    using SchoolAssistant.Data.Models;
    using SchoolAssistant.Services.Mapping;

    public class CourseInListViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public string Description { get; set; }

        public string CreatedByUserUserName { get; set; }
    }
}
