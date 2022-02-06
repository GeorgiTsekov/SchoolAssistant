namespace SchoolAssistant.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;

    using SchoolAssistant.Data.Models;
    using SchoolAssistant.Services.Mapping;

    public class SingleCourseViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string DepartmentName { get; set; }

        public string Description { get; set; }

        public string CreatedByUserUserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public IEnumerable<LecturesViewModel> Lectures { get; set; }
    }
}
