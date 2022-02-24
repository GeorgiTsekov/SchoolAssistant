namespace SchoolAssistant.Web.ViewModels.SearchCourses
{
    using SchoolAssistant.Data.Models;
    using SchoolAssistant.Services.Mapping;

    public class LecturesNameIdViewModel : IMapFrom<Lecture>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
