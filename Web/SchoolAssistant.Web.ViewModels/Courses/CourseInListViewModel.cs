namespace SchoolAssistant.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CourseInListViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public string Description { get; set; }

        public string CreatedByUserName { get; set; }
    }
}
