namespace SchoolAssistant.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using SchoolAssistant.Data;
    using SchoolAssistant.Data.Models;
    using SchoolAssistant.Services.Mapping;
    using SchoolAssistant.Web.ViewModels.Courses;

    public class CoursesService : ICoursesService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif", "pdf", "txt", "docx", "pptx" };
        private readonly ApplicationDbContext applicationDbContext;

        public CoursesService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
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

            await this.applicationDbContext.Courses.AddAsync(course);
            await this.applicationDbContext.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12)
        {
            var courses = this.applicationDbContext.Courses
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();

            return courses;
        }

        public T GetById<T>(int id)
        {
            var course = this.applicationDbContext.Courses
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return course;
        }

        public T GetLectureById<T>(int id)
        {
            var lecture = this.applicationDbContext.Lectures
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return lecture;
        }

        public int GetCount()
        {
            return this.applicationDbContext.Courses.Count();
        }

        public IEnumerable<T> GetRandom<T>(int count)
        {
            return this.applicationDbContext.Courses
                .OrderBy(x => Guid.NewGuid())
                .Take(count)
                .To<T>()
                .ToList();
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
