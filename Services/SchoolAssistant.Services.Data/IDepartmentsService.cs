namespace SchoolAssistant.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SchoolAssistant.Data.Models;

    public interface IDepartmentsService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();

        Task CreateAsync(Department department);

        IEnumerable<Department> GetAll();

        Task<Department> GetById(int? id);

        Task<Department> GetByIdAndUpdate(int? id, Department department);
    }
}
