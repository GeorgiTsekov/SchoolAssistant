namespace SchoolAssistant.Web.ViewModels.Courses
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreateCourseInputModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [MinLength(25)]
        public string Description { get; set; }

        public int DepartmentId { get; set; }

        public virtual IEnumerable<KeyValuePair<string, string>> DepartmentsItems { get; set; }
    }
}
