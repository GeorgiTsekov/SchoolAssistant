namespace SchoolAssistant.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using SchoolAssistant.Data.Models;

    public class DepartmentsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Department.Any())
            {
                return;
            }

            await dbContext.Department.AddAsync(new Department { Name = "Faculty of Automatics" });
            await dbContext.Department.AddAsync(new Department { Name = "Faculty of Electrical Engineering" });
            await dbContext.Department.AddAsync(new Department { Name = "Faculty of Power Engineering and Power Machines" });
            await dbContext.Department.AddAsync(new Department { Name = "Faculty of Industrial Technology" });
            await dbContext.Department.AddAsync(new Department { Name = "Faculty of Mechanical Engineering" });
            await dbContext.Department.AddAsync(new Department { Name = "Faculty of Electronic Engineering and Technologies" });
            await dbContext.Department.AddAsync(new Department { Name = "Faculty of Telecommunications" });
            await dbContext.Department.AddAsync(new Department { Name = "Faculty of Computer Systems and Technologies" });
            await dbContext.Department.AddAsync(new Department { Name = "Faculty of Transport" });
            await dbContext.Department.AddAsync(new Department { Name = "Faculty of Management" });
            await dbContext.Department.AddAsync(new Department { Name = "Faculty of Applied Mathematics and Informatics" });
            await dbContext.Department.AddAsync(new Department { Name = "Faculty of German Engineering Education and Industrial Management" });
            await dbContext.Department.AddAsync(new Department { Name = "French Faculty of Electrical Engineering" });
            await dbContext.Department.AddAsync(new Department { Name = "English Language Faculty of Engineering" });

            await dbContext.SaveChangesAsync();
        }
    }
}
