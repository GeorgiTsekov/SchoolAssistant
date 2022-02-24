namespace SchoolAssistant.Web.ViewModels.SearchCourses
{
    using System.Collections.Generic;

    using SchoolAssistant.Web.ViewModels.Courses;

    public class ListViewModel
    {
        public IEnumerable<CourseInListViewModel> Courses { get; set; }
    }
}
