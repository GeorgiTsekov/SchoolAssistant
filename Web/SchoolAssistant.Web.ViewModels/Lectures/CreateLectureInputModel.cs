namespace SchoolAssistant.Web.ViewModels.Lectures
{
    using SchoolAssistant.Data.Models;
    using SchoolAssistant.Services.Mapping;

    public class CreateLectureInputModel : CourseLectureInputModel, IMapFrom<Course>
    {
        public int Id { get; set; }

        public string CreatedByUserUserName { get; set; }
    }
}
