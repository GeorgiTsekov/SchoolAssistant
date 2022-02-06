namespace SchoolAssistant.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using SchoolAssistant.Data.Common.Repositories;
    using SchoolAssistant.Data.Models;
    using SchoolAssistant.Services.Mapping;
    using SchoolAssistant.Web.ViewModels.Courses;

    public class CoursesService : ICoursesService
    {
        private readonly IDeletableEntityRepository<Course> courseRepository;
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif", "pdf", "txt", "docx", "pptx" };

        public CoursesService(IDeletableEntityRepository<Course> courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public async Task CreateAsync(CreateCourseInputModel input, string userId, string presentationPath)
        {
            var course = new Course
            {
                Name = input.Name,
                Description = input.Description,
                DepartmentId = input.DepartmentId,
                CreatedByUserId = userId,
            };

            foreach (var lectureInput in input.Lectures)
            {
                var lecture = new Lecture
                {
                    Name = lectureInput.Name,
                    VideoUrl = lectureInput.VideoUrl,
                };

                Directory.CreateDirectory($"{presentationPath}/lectures/courses/");
                foreach (var presentationInput in lectureInput.Presentations)
                {
                    var extension = Path.GetExtension(presentationInput.FileName).TrimStart('.');

                    if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                    {
                        throw new Exception($"Invalid image extension {extension}");
                    }

                    var dbPresentation = new Presentation
                    {
                        CreatedByUserId = userId,
                        Extension = extension,
                    };

                    lecture.Presentations.Add(dbPresentation);
                    var physicalPath = $"{presentationPath}/lectures/courses/{dbPresentation.Id}.{extension}";
                    using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                    await presentationInput.CopyToAsync(fileStream);
                }

                course.Lectures.Add(lecture);
            }

            await this.courseRepository.AddAsync(course);
            await this.courseRepository.SaveChangesAsync();
        }

        public IEnumerable<CourseInListViewModel> GetAll(int page, int itemsPerPage = 12)
        {
            var courses = this.courseRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new CourseInListViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    DepartmentName = x.Department.Name,
                    DepartmentId = x.DepartmentId,
                    Description = x.Description,
                    CreatedByUserUserName = x.CreatedByUser.UserName,
                })
                .ToList();

            return courses;
        }

        public T GetById<T>(int id)
        {
            var course = this.courseRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return course;
        }

        public int GetCount()
        {
            return this.courseRepository.All().Count();
        }
    }
}
