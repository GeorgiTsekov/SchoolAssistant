namespace SchoolAssistant.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using SchoolAssistant.Web.ViewModels.Courses;

    public class IndexViewModel
    {
        public IEnumerable<CourseInListViewModel> RandomCourses { get; set; }

        public int CoursesCount { get; set; }

        public int DepartmentsCount { get; set; }
    }
}
