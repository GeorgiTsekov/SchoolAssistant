namespace SchoolAssistant.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SchoolAssistant.Data.Common.Repositories;
    using SchoolAssistant.Data.Models;

    public class DepartmentsService : IDepartmentsService
    {
        private readonly IDeletableEntityRepository<Department> departmentRepository;

        public DepartmentsService(IDeletableEntityRepository<Department> departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        public async Task CreateAsync(Department department)
        {
            await this.departmentRepository.AddAsync(department);
            await this.departmentRepository.SaveChangesAsync();
        }

        public IEnumerable<Department> GetAll()
        {
            return this.departmentRepository.AllWithDeleted().ToList();
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.departmentRepository.AllAsNoTracking()
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                })
                .ToList()
                .OrderBy(x => x.Name)
                .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public async Task<Department> GetById(int? id)
        {
            if (id == null)
            {
                throw new Exception($"Invalid department");
            }

            var department = await this.departmentRepository.AllWithDeleted()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (department == null)
            {
                throw new Exception($"Invalid department");
            }

            return department;
        }

        public Task<Department> GetByIdAndUpdate(int? id, Department department)
        {
            throw new NotImplementedException();
        }
    }
}
