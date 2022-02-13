namespace SchoolAssistant.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using SchoolAssistant.Data;
    using SchoolAssistant.Data.Common.Repositories;
    using SchoolAssistant.Data.Models;
    using SchoolAssistant.Web.ViewModels.Courses;
    using SchoolAssistant.Web.Views.Courses;
    using Xunit;

    public class CoursesServiceTests
    {
        [Fact]
        public void TestGetCount_WithTestedData_ShouldReturnCountOfCourses()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);
            SeedTestData(context);

            var coursesService = new CoursesService(context);

            var expectedData = GetTestData();
            var actualDataCount = coursesService.GetCount();

            Assert.True(expectedData.Count == actualDataCount, "CourseService GetCount() method does not work properly!");
        }

        [Fact]
        public void TestGetAll_WithTestedData_ShouldReturnCourseGetAll()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);
            SeedTestData(context);

            var coursesService = new CoursesService(context);

            var expectedData = GetTestData();
            var actualData = coursesService.GetAll<Course>(1, 12);

            Assert.True(expectedData == actualData, "CourseService CourseById() method does not work properly!");
        }

        [Fact]
        public void TestGetById_WithTestedData_ShouldReturnCourseById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);
            SeedTestData(context);

            var coursesService = new CoursesService(context);

            var expectedData = GetTestData().FirstOrDefault(x => x.Id == 2);
            var actualDataCount = coursesService.GetById<Course>(2);

            Assert.True(expectedData.Name == actualDataCount.Name, "CourseService CourseById() method does not work properly!");
        }

        [Fact]
        public void TestGetCountCourses_WithoutAnyData_ShouldReturnZero()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);

            var coursesService = new CoursesService(context);

            var actualDataCount = coursesService.GetCount();

            Assert.True(actualDataCount == 0, "CourseService GetCount() method does not work properly!");
        }

        private static void SeedTestData(ApplicationDbContext context)
        {
            context.Courses.AddRange(GetTestData());
            context.SaveChanges();
        }

        private static List<Course> GetTestData()
        {
            return new List<Course>
            {
                new Course
                {
                    Name = "Pesho",
                    Description = "1asdjlask ljdakl sjkajd kasjd klasj kdjaldk jasld",
                    DepartmentId = 1,
                    Id = 1,
                    CreatedByUserId = "Tosho123",
                },
                new Course
                {
                    Name = "Gosho",
                    Description = "2sdfdfg2 dfgdgssafjjk ljdakl sjkajd kasjd klasj kdjaldk jasld",
                    DepartmentId = 2,
                    Id = 2,
                },
            };
        }

        ////[Fact]
        ////public async Task CreateAsyncShoutWorksCorrectly()
        ////{
        ////    var courses = new List<Course>();
        ////    CreateCoursesServiceTest(courses, out Mock<IDeletableEntityRepository<Course>> mockCoursesRepo, out CoursesService service, out CreateCourseInputModel courseInputModel);

        ////    await service.CreateAsync(courseInputModel, Guid.NewGuid().ToString(), "../presentations/lectures/courses/");
        ////    await service.CreateAsync(courseInputModel, Guid.NewGuid().ToString(), "../presentations/lectures/courses/");
        ////    await service.CreateAsync(courseInputModel, Guid.NewGuid().ToString(), "../presentations/lectures/courses/");

        ////    Assert.Equal(3, service.GetCount());

        ////    await service.CreateAsync(courseInputModel, Guid.NewGuid().ToString(), "../presentations/lectures/courses/");

        ////    mockCoursesRepo.Verify(x => x.AddAsync(It.IsAny<Course>()), Times.Exactly(4));
        ////    Assert.Equal(4, service.GetCount());

        ////    ////foreach (var course in service.GetAll<Course>(1, 12))
        ////    ////{
        ////    ////    Assert.True(courses.Any(c => c.Name == course.Name), "CoursesService GetAll() method bla bla bla");
        ////    ////}
        ////}

        ////[Fact]
        ////public async Task GetByIdShouldWorksCorrectly()
        ////{
        ////    var courses = new List<Course>();
        ////    CreateCoursesServiceTest(courses, out Mock<IDeletableEntityRepository<Course>> mockCoursesRepo, out CoursesService service, out CreateCourseInputModel courseInputModel);

        ////    await service.CreateAsync(courseInputModel, Guid.NewGuid().ToString(), "../presentations/lectures/courses/");

        ////    var course = courses.FirstOrDefault(x => x.Name == "Pesho");
        ////    service.GetById<Course>(course.Id).Name.Equals("Pesho");
        ////    var currentCourse = service.GetById<Course>(course.Id);
        ////    Assert.Equal("Pesho", currentCourse.Name);
        ////}

        ////[Fact]
        ////public async Task GetAllShouldWorksCorrectly()
        ////{
        ////    var courses = new List<Course>();
        ////    var mockCoursesRepo = new Mock<IDeletableEntityRepository<Course>>();
        ////    mockCoursesRepo.Setup(x => x.All()).Returns(courses.AsQueryable());
        ////    mockCoursesRepo.Setup(x => x.AddAsync(It.IsAny<Course>())).Callback((Course course) => courses.Add(course));
        ////    mockCoursesRepo.Setup(x => x.All()).Returns(courses.AsQueryable());

        ////    var lectures = new List<Lecture>();
        ////    var mockLecturesRepo = new Mock<IDeletableEntityRepository<Lecture>>();
        ////    mockLecturesRepo.Setup(x => x.All()).Returns(lectures.AsQueryable());
        ////    mockLecturesRepo.Setup(x => x.AddAsync(It.IsAny<Lecture>())).Callback((Lecture lecture) => lectures.Add(lecture));
        ////    mockLecturesRepo.Setup(x => x.All()).Returns(lectures.AsQueryable());

        ////    var service = new CoursesService(mockCoursesRepo.Object, mockLecturesRepo.Object);

        ////    var content = "Hello World from a Fake File";
        ////    var fileName = "test.pdf";
        ////    var stream = new MemoryStream();
        ////    var writer = new StreamWriter(stream);
        ////    writer.Write(content);
        ////    writer.Flush();
        ////    stream.Position = 0;
        ////    IFormFile presentation = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);
        ////    var presentations = new List<IFormFile>
        ////    {
        ////        presentation,
        ////    };
        ////    var lectureInputModel = new CourseLectureInputModel
        ////    {
        ////        Name = Guid.NewGuid().ToString(),
        ////        VideoUrl = "https://www.youtube.com/watch?v=oneJK8gyUGc&t=6s",
        ////        Presentations = presentations,
        ////    };
        ////    var lecturesInputModel = new List<CourseLectureInputModel>
        ////    {
        ////        lectureInputModel,
        ////    };

        ////    var courseInputModel = new CreateCourseInputModel
        ////    {
        ////        Name = "Pesho",
        ////        DepartmentId = 1,
        ////        Description = "asdasd;lsd jls fklsd fjsklfj sdkl fjsdkl fjsdkj fsdklfj klsd fjsd",
        ////        Lectures = lecturesInputModel,
        ////    };

        ////    await service.CreateAsync(courseInputModel, Guid.NewGuid().ToString(), "../presentations/lectures/courses/");
        ////    await service.CreateAsync(courseInputModel, Guid.NewGuid().ToString(), "../presentations/lectures/courses/");
        ////    await service.CreateAsync(courseInputModel, Guid.NewGuid().ToString(), "../presentations/lectures/courses/");
        ////    var asd = service.GetCount();
        ////    var dsa = service.GetAll<Course>(1, 12).Count();
        ////}

        ////private static void CreateCoursesServiceTest(List<Course> courses, out Mock<IDeletableEntityRepository<Course>> mockCoursesRepo, out CoursesService service, out CreateCourseInputModel courseInputModel)
        ////{
        ////    mockCoursesRepo = new Mock<IDeletableEntityRepository<Course>>();
        ////    mockCoursesRepo.Setup(x => x.All()).Returns(courses.AsQueryable());
        ////    mockCoursesRepo.Setup(x => x.AddAsync(It.IsAny<Course>())).Callback((Course course) => courses.Add(course));

        ////    var lectures = new List<Lecture>();
        ////    var mockLecturesRepo = new Mock<IDeletableEntityRepository<Lecture>>();
        ////    mockLecturesRepo.Setup(x => x.All()).Returns(lectures.AsQueryable());
        ////    mockLecturesRepo.Setup(x => x.AddAsync(It.IsAny<Lecture>())).Callback((Lecture lecture) => lectures.Add(lecture));

        ////    service = new CoursesService(mockCoursesRepo.Object, mockLecturesRepo.Object);
        ////    courseInputModel = CourseInputModel();
        ////}

        ////private static CreateCourseInputModel CourseInputModel()
        ////{
        ////    var content = "Hello World from a Fake File";
        ////    var fileName = "test.pdf";
        ////    var stream = new MemoryStream();
        ////    var writer = new StreamWriter(stream);
        ////    writer.Write(content);
        ////    writer.Flush();
        ////    stream.Position = 0;
        ////    IFormFile presentation = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);
        ////    var presentations = new List<IFormFile>
        ////    {
        ////        presentation,
        ////    };
        ////    var lectureInputModel = new CourseLectureInputModel
        ////    {
        ////        Name = Guid.NewGuid().ToString(),
        ////        VideoUrl = "https://www.youtube.com/watch?v=oneJK8gyUGc&t=6s",
        ////        Presentations = presentations,
        ////    };
        ////    var lecturesInputModel = new List<CourseLectureInputModel>
        ////    {
        ////        lectureInputModel,
        ////    };

        ////    var courseInputModel = new CreateCourseInputModel
        ////    {
        ////        Name = "Pesho",
        ////        DepartmentId = 1,
        ////        Description = "asdasd;lsd jls fklsd fjsklfj sdkl fjsdkl fjsdkj fsdklfj klsd fjsd",
        ////        Lectures = lecturesInputModel,
        ////    };
        ////    return courseInputModel;
        ////}
    }
}
