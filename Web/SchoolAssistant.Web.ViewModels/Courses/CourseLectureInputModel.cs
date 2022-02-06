namespace SchoolAssistant.Web.Views.Courses
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CourseLectureInputModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [Url]
        public string VideoUrl { get; set; }

        public IEnumerable<IFormFile> Presentations { get; set; }
    }
}
