namespace SchoolAssistant.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using SchoolAssistant.Data.Common.Repositories;
    using SchoolAssistant.Data.Models;

    public class DepartmentsService : IDepartmentsService
    {
        private readonly IDeletableEntityRepository<Department> departmentRepository;

        public DepartmentsService(IDeletableEntityRepository<Department> departmentRepository)
        {
            this.departmentRepository = departmentRepository;
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
    }
}
