namespace SchoolAssistant.Web.ViewModels.Courses
{
    using System.Collections.Generic;

    using SchoolAssistant.Data.Models;
    using SchoolAssistant.Services.Mapping;
    using SchoolAssistant.Web.Views.Courses;

    public class CreateCourseInputModel : BaseCourseInputModel, IMapFrom<Course>
    {
        public IEnumerable<CourseLectureInputModel> Lectures { get; set; }
    }
}
