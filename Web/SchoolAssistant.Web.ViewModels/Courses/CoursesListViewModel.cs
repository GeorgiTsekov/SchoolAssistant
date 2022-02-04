namespace SchoolAssistant.Web.ViewModels.Courses
{
    using System.Collections.Generic;

    public class CoursesListViewModel : PagingViewModel
    {
        public IEnumerable<CourseInListViewModel> Courses { get; set; }
    }
}
