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

            await dbContext.Department.AddAsync(new Department { Name = "Mathematic" });
            await dbContext.Department.AddAsync(new Department { Name = "Chemistry" });
            await dbContext.Department.AddAsync(new Department { Name = "It" });
            await dbContext.Department.AddAsync(new Department { Name = "Iconomy" });
            await dbContext.Department.AddAsync(new Department { Name = "Phisic" });
            await dbContext.Department.AddAsync(new Department { Name = "Philosophy" });
            await dbContext.Department.AddAsync(new Department { Name = "Engeneering" });

            await dbContext.SaveChangesAsync();
        }
    }
}
