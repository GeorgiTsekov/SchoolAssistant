namespace SchoolAssistant.Web.Views.Courses
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SchoolAssistant.Web.ViewModels.Courses;

    public class CourseLectureInputModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [Url]
        public string VideoUrl { get; set; }

        public int CourseId { get; set; }

        public ICollection<CourseLecturePresentationInputModel> Presentations { get; set; }
    }
}
