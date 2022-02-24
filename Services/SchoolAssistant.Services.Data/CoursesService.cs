namespace SchoolAssistant.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using SchoolAssistant.Data;
    using SchoolAssistant.Data.Common.Repositories;
    using SchoolAssistant.Data.Models;
    using SchoolAssistant.Services.Mapping;
    using SchoolAssistant.Web.ViewModels.Courses;
    using SchoolAssistant.Web.ViewModels.Lectures;

    public class CoursesService : ICoursesService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif", "pdf", "txt", "docx", "pptx" };
        private readonly IDeletableEntityRepository<Course> coursesRepository;
        private readonly IDeletableEntityRepository<Lecture> lecturesRepository;

        public CoursesService(
            IDeletableEntityRepository<Course> coursesRepository,
            IDeletableEntityRepository<Lecture> lecturesRepository)
        {
            this.coursesRepository = coursesRepository;
            this.lecturesRepository = lecturesRepository;
        }

        public async Task CreateAsync(CreateCourseInputModel input, string userId)
        {
            var course = new Course
            {
                Name = input.Name,
                Description = input.Description,
                DepartmentId = input.DepartmentId,
                CreatedByUserId = userId,
            };

            await this.coursesRepository.AddAsync(course);
            await this.coursesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12)
        {
            var courses = this.coursesRepository.All()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();

            return courses;
        }

        public T GetById<T>(int id)
        {
            var course = this.coursesRepository.All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return course;
        }

        public T GetLectureById<T>(int id)
        {
            var lecture = this.lecturesRepository.All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return lecture;
        }

        public int GetCount()
        {
            return this.coursesRepository.All().Count();
        }

        public IEnumerable<T> GetRandom<T>(int count)
        {
            return this.coursesRepository.All()
                .OrderBy(x => Guid.NewGuid())
                .Take(count)
                .To<T>()
                .ToList();
        }

        public async Task UpdateAsync(int id, EditCourseInputModel input)
        {
            var course = this.coursesRepository.All().FirstOrDefault(x => x.Id == id);
            course.Name = input.Name;
            course.Description = input.Description;
            course.DepartmentId = input.DepartmentId;
            await this.coursesRepository.SaveChangesAsync();
        }

        public async Task AddLectureAsync(int id, CreateLectureInputModel input, string presentationPath)
        {
            var course = this.coursesRepository.All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            var videoInput = input.VideoUrl;
            string youtubeVideo = MakeYoutubeVideoWorkForMyApp(videoInput);

            var lecture = new Lecture
            {
                Name = input.LectureName,
                VideoUrl = youtubeVideo.ToString().TrimEnd(),
            };

            Directory.CreateDirectory($"{presentationPath}/lectures/courses/");
            foreach (var presentationInput in input.Presentations)
            {
                var extension = Path.GetExtension(presentationInput.FileName).TrimStart('.');

                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extension}");
                }

                var dbPresentation = new Presentation
                {
                    CreatedByUserId = input.CreatedByUserUserName,
                    Extension = extension,
                };

                lecture.Presentations.Add(dbPresentation);
                var physicalPath = $"{presentationPath}/lectures/courses/{dbPresentation.Id}.{extension}";
                using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                await presentationInput.CopyToAsync(fileStream);
            }

            course.Lectures.Add(lecture);
            await this.coursesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetCoursesByLectureName<T>(string name)
        {
            var lectures = this.lecturesRepository.All();
            var result = this.coursesRepository.All().AsQueryable();

            if (name == null)
            {
                return result.AsQueryable().To<T>().ToList();
            }

            var coursesIds = new List<int>();

            foreach (var lecture in lectures)
            {
                if (lecture.Name.Contains(name))
                {
                    if (!coursesIds.Contains(lecture.CourseId))
                    {
                        coursesIds.Add(lecture.CourseId);
                    }
                }
            }

            result = result.Where(x => coursesIds.Any(c => c == x.Id));
            return result.AsQueryable().To<T>().ToList();
        }

        public async Task DeleteAsync(int id)
        {
            var course = this.coursesRepository.All().FirstOrDefault(x => x.Id == id);
            this.coursesRepository.Delete(course);
            await this.coursesRepository.SaveChangesAsync();
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
