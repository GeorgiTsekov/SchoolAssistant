namespace SchoolAssistant.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using SchoolAssistant.Data.Common.Repositories;
    using SchoolAssistant.Data.Models;
    using SchoolAssistant.Services.Mapping;
    using SchoolAssistant.Web.ViewModels.Courses;

    public class CoursesService : ICoursesService
    {
        private readonly IDeletableEntityRepository<Course> courseRepository;
        private readonly IDeletableEntityRepository<Lecture> lectureRepository;
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif", "pdf", "txt", "docx", "pptx" };

        public CoursesService(
            IDeletableEntityRepository<Course> courseRepository,
            IDeletableEntityRepository<Lecture> lectureRepository)
        {
            this.courseRepository = courseRepository;
            this.lectureRepository = lectureRepository;
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
                var videoInput = lectureInput.VideoUrl;
                string youtubeVideo = MakeYoutubeVideoWorkForMyApp(videoInput);

                var lecture = new Lecture
                {
                    Name = lectureInput.Name,
                    VideoUrl = youtubeVideo.ToString().TrimEnd(),
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

        public T GetLectureById<T>(int id)
        {
            var lecture = this.lectureRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return lecture;
        }

        public int GetCount()
        {
            return this.courseRepository.All().Count();
        }

        private static string MakeYoutubeVideoWorkForMyApp(string videoInput)
        {
            var sb = new StringBuilder();
            sb.Append("https://www.youtube.com/embed/");
            var splitedVideoUrl = videoInput.Split("=");
            sb.Append(splitedVideoUrl[1]);
            sb.Append("?autoplay=0");
            var youtubeVideo = sb.ToString().TrimEnd();
            return youtubeVideo;
        }
    }
}
