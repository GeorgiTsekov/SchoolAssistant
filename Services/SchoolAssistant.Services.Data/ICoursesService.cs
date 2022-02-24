namespace SchoolAssistant.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SchoolAssistant.Web.ViewModels.Courses;
    using SchoolAssistant.Web.ViewModels.Lectures;

    public interface ICoursesService
    {
        Task CreateAsync(CreateCourseInputModel input, string userId);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        int GetCount();

        T GetById<T>(int id);

        T GetLectureById<T>(int id);

        IEnumerable<T> GetRandom<T>(int count);

        Task UpdateAsync(int id, EditCourseInputModel input);

        Task AddLectureAsync(int id, CreateLectureInputModel input, string presentationPath);

        IEnumerable<T> GetCoursesByLectureName<T>(string name);
    }
}
