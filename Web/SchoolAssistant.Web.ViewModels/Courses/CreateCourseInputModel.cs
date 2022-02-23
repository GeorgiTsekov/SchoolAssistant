namespace SchoolAssistant.Web.ViewModels.Courses
{
    using SchoolAssistant.Data.Models;
    using SchoolAssistant.Services.Mapping;

    public class CreateCourseInputModel : BaseCourseInputModel, IMapFrom<Course>
    {
    }
}
