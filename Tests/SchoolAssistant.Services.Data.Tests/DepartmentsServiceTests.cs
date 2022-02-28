namespace SchoolAssistant.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using Moq;
    using SchoolAssistant.Data.Common.Repositories;
    using SchoolAssistant.Data.Models;
    using SchoolAssistant.Services.Mapping;
    using SchoolAssistant.Web.ViewModels;
    using Xunit;

    public class DepartmentsServiceTests
    {
        public DepartmentsServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public void TestGetCountCourses_WithoutAnyData_ShouldReturnZero()
        {
            var mockDepartmentRepo = new Mock<IDeletableEntityRepository<Department>>();
            mockDepartmentRepo.Setup(x => x.AllWithDeleted()).Returns(new List<Department>
            {
                new Department
                {
                    Id = 1,
                    Name = "Pesho",
                },
                new Department
                {
                    Id = 2,
                    Name = "Gosho",
                },
            }.AsQueryable());

            IDepartmentsService departmentsService = new DepartmentsService(mockDepartmentRepo.Object);
            var allDepartments = departmentsService.GetAll();
            Assert.Equal(2, allDepartments.Count());
        }
    }
}
