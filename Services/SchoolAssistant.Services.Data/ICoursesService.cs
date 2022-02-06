﻿namespace SchoolAssistant.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SchoolAssistant.Web.ViewModels.Courses;

    public interface ICoursesService
    {
        Task CreateAsync(CreateCourseInputModel input, string userId, string presentationPath);

        IEnumerable<CourseInListViewModel> GetAll(int page, int itemsPerPage = 12);

        int GetCount();
    }
}
