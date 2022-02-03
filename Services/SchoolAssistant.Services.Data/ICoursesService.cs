namespace SchoolAssistant.Services.Data
{
    using System.Threading.Tasks;

    using SchoolAssistant.Web.ViewModels.Courses;

    public interface ICoursesService
    {
        Task CreateAsync(CreateCourseInputModel input);
    }
}
