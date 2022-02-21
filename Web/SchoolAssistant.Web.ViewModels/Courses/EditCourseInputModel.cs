namespace SchoolAssistant.Web.ViewModels.Courses
{
    using SchoolAssistant.Data.Models;
    using SchoolAssistant.Services.Mapping;

    public class EditCourseInputModel : BaseCourseInputModel, IMapFrom<Course>
    {
        public int Id { get; set; }

        public string CreatedByUserUserName { get; set; }
    }
}
