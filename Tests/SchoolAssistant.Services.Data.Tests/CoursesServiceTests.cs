namespace SchoolAssistant.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using SchoolAssistant.Data;
    using SchoolAssistant.Data.Common.Repositories;
    using SchoolAssistant.Data.Models;
    using SchoolAssistant.Services.Mapping;
    using SchoolAssistant.Web.ViewModels;
    using SchoolAssistant.Web.ViewModels.Courses;
    using SchoolAssistant.Web.ViewModels.Lectures;
    using Xunit;

    public class CoursesServiceTests
    {
        public CoursesServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public void TestGetById_WithTestedData_ShouldReturnCourseById()
        {
            var mockCourseRepo = new Mock<IDeletableEntityRepository<Course>>();
            var mockLectureRepo = new Mock<IDeletableEntityRepository<Lecture>>();
            mockCourseRepo.Setup(x => x.AllAsNoTracking()).Returns(GetTestCourseData().AsQueryable());
            var lectures = new List<Lecture>();
            mockLectureRepo.Setup(x => x.AllAsNoTracking()).Returns(lectures.AsQueryable());
            var coursesService = new CoursesService(mockCourseRepo.Object, mockLectureRepo.Object);
            var currentCourses = coursesService.GetById<SingleCourseViewModel>(11);
            Assert.Equal("Pesho", currentCourses.Name);
        }

        [Fact]
        public void TestGetCountCourses_WithoutAnyData_ShouldReturnZero()
        {
            var mockCourseRepo = new Mock<IDeletableEntityRepository<Course>>();
            var mockLectureRepo = new Mock<IDeletableEntityRepository<Lecture>>();
            var courses = new List<Course>();
            mockCourseRepo.Setup(x => x.AllAsNoTracking()).Returns(courses.AsQueryable());
            var lectures = new List<Lecture>();
            mockLectureRepo.Setup(x => x.AllAsNoTracking()).Returns(lectures.AsQueryable());
            var coursesService = new CoursesService(mockCourseRepo.Object, mockLectureRepo.Object);
            var allCourses = coursesService.GetAll<CourseInListViewModel>(1, 12);
            Assert.Empty(allCourses);
        }

        [Fact]
        public void TestGetAll_WithTestedData_ShouldReturnCourseGetAll()
        {
            var mockCourseRepo = new Mock<IDeletableEntityRepository<Course>>();
            var mockLectureRepo = new Mock<IDeletableEntityRepository<Lecture>>();
            mockCourseRepo.Setup(x => x.AllAsNoTracking()).Returns(GetTestCourseData().AsQueryable());
            var lectures = new List<Lecture>();
            mockLectureRepo.Setup(x => x.AllAsNoTracking()).Returns(lectures.AsQueryable());
            var coursesService = new CoursesService(mockCourseRepo.Object, mockLectureRepo.Object);
            var allCourses = coursesService.GetAll<CourseInListViewModel>(1, 12);
            Assert.Equal(2, allCourses.Count());
        }

        [Fact]
        public async void TestCreateAsync_WithTestedData_ShouldWorkCorrectly()
        {
            var courses = new List<Course>();
            CreateCoursesServiceTest(courses, out Mock<IDeletableEntityRepository<Course>> mockCoursesRepo, out CoursesService service, out CreateCourseInputModel courseInputModel);

            await service.CreateAsync(courseInputModel, Guid.NewGuid().ToString());
            await service.CreateAsync(courseInputModel, Guid.NewGuid().ToString());
            await service.CreateAsync(courseInputModel, Guid.NewGuid().ToString());

            Assert.Equal(3, service.GetCount());

            await service.CreateAsync(courseInputModel, Guid.NewGuid().ToString());

            mockCoursesRepo.Verify(x => x.AddAsync(It.IsAny<Course>()), Times.Exactly(4));
            Assert.Equal(4, service.GetCount());
        }

        private static List<Course> GetTestCourseData()
        {
            return new List<Course>
            {
                new Course
                {
                    Name = "Pesho",
                    CreatedByUser = new ApplicationUser
                    {
                        UserName = "Pesho1",
                        Id = "Tosho123",
                    },
                    Description = "1asdjlask ljdakl sjkajd kasjd klasj kdjaldk jasld",
                    Department = new Department
                    {
                        Id = 1,
                        Name = "asdasd",
                    },
                    Id = 11,
                },
                new Course
                {
                    Name = "Gosho",
                    CreatedByUser = new ApplicationUser
                    {
                        UserName = "Pesho1",
                        Id = "Gisho123",
                    },
                    Description = "2sdfdfg2 dfgdgssafjjk ljdakl sjkajd kasjd klasj kdjaldk jasld",
                    Department = new Department
                    {
                        Id = 2,
                        Name = "Bsdasd",
                    },
                    Id = 12,
                },
            };
        }

        private static void CreateCoursesServiceTest(List<Course> courses, out Mock<IDeletableEntityRepository<Course>> mockCoursesRepo, out CoursesService service, out CreateCourseInputModel courseInputModel)
        {
            mockCoursesRepo = new Mock<IDeletableEntityRepository<Course>>();
            mockCoursesRepo.Setup(x => x.AllAsNoTracking()).Returns(courses.AsQueryable());
            mockCoursesRepo.Setup(x => x.AddAsync(It.IsAny<Course>())).Callback((Course course) => courses.Add(course));

            var lectures = new List<Lecture>();
            var mockLecturesRepo = new Mock<IDeletableEntityRepository<Lecture>>();
            mockLecturesRepo.Setup(x => x.AllAsNoTracking()).Returns(lectures.AsQueryable());
            mockLecturesRepo.Setup(x => x.AddAsync(It.IsAny<Lecture>())).Callback((Lecture lecture) => lectures.Add(lecture));

            service = new CoursesService(mockCoursesRepo.Object, mockLecturesRepo.Object);
            courseInputModel = CourseInputModel();
        }

        private static CreateCourseInputModel CourseInputModel()
        {
            var content = "Hello World from a Fake File";
            var fileName = "test.pdf";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;
            IFormFile presentation = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);
            var presentations = new List<IFormFile>
            {
                presentation,
            };
            var lectureInputModel = new CourseLectureInputModel
            {
                LectureName = Guid.NewGuid().ToString(),
                VideoUrl = "https://www.youtube.com/watch?v=oneJK8gyUGc&t=6s",
                Presentations = presentations,
            };
            var lecturesInputModel = new List<CourseLectureInputModel>
            {
                lectureInputModel,
            };

            var courseInputModel = new CreateCourseInputModel
            {
                Name = "Pesho",
                DepartmentId = 1,
                Description = "asdasd;lsd jls fklsd fjsklfj sdkl fjsdkl fjsdkj fsdklfj klsd fjsd",
            };
            return courseInputModel;
        }
    }
}
