namespace SchoolAssistant.Web.Views.Courses
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using SchoolAssistant.Data.Models;
    using SchoolAssistant.Services.Mapping;

    public class CourseLectureInputModel : IMapFrom<Lecture>
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
