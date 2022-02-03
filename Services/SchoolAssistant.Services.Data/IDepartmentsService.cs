namespace SchoolAssistant.Services.Data
{
    using System.Collections.Generic;

    public interface IDepartmentsService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
