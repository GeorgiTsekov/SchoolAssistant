namespace SchoolAssistant.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Moq;
    using SchoolAssistant.Data.Common.Repositories;
    using SchoolAssistant.Data.Models;
    using SchoolAssistant.Web.ViewModels.Courses;
    using SchoolAssistant.Web.Views.Courses;
    using Xunit;

    public class CoursesServiceTests
    {
        [Fact]
        public async Task WhenCourseIsCreatedCountShoutOfCourseShoutBeUpdatedPlusOneMoreCount()
        {
            var courses = new List<Course>();
            var mockCoursesRepo = new Mock<IDeletableEntityRepository<Course>>();
            mockCoursesRepo.Setup(x => x.All()).Returns(courses.AsQueryable());
            mockCoursesRepo.Setup(x => x.AddAsync(It.IsAny<Course>())).Callback((Course course) => courses.Add(course));

            var lectures = new List<Lecture>();
            var mockLecturesRepo = new Mock<IDeletableEntityRepository<Lecture>>();
            mockLecturesRepo.Setup(x => x.All()).Returns(lectures.AsQueryable());
            mockLecturesRepo.Setup(x => x.AddAsync(It.IsAny<Lecture>())).Callback((Lecture lecture) => lectures.Add(lecture));

            var service = new CoursesService(mockCoursesRepo.Object, mockLecturesRepo.Object);
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
                Name = Guid.NewGuid().ToString(),
                VideoUrl = "https://www.youtube.com/watch?v=oneJK8gyUGc&t=6s",
                Presentations = presentations,
            };
            var list = new List<CourseLectureInputModel>
            {
                lectureInputModel,
            };
            var courseInputModel = new CreateCourseInputModel
            {
                Name = Guid.NewGuid().ToString(),
                DepartmentId = 1,
                Description = "asdasd;lsd jls fklsd fjsklfj sdkl fjsdkl fjsdkj fsdklfj klsd fjsd",
                Lectures = list,
            };

            await service.CreateAsync(courseInputModel, Guid.NewGuid().ToString(), "../presentations/lectures/courses/");
            await service.CreateAsync(courseInputModel, Guid.NewGuid().ToString(), "../presentations/lectures/courses/");
            await service.CreateAsync(courseInputModel, Guid.NewGuid().ToString(), "../presentations/lectures/courses/");
            Assert.Equal(3, courses.Count);

            await service.CreateAsync(courseInputModel, Guid.NewGuid().ToString(), "../presentations/lectures/courses/");

            mockCoursesRepo.Verify(x => x.AddAsync(It.IsAny<Course>()), Times.Exactly(4));
            Assert.Equal(4, courses.Count);
        }
    }
}
