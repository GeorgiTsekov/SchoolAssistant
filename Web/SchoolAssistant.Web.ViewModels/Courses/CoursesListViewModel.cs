namespace SchoolAssistant.Web.ViewModels.Courses
{
    using System.Collections.Generic;

    public class CoursesListViewModel
    {
        public IEnumerable<CourseInListViewModel> Courses { get; set; }

        public int PageNumber { get; set; }
    }
}
