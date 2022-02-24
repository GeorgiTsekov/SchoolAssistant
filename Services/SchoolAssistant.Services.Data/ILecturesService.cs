namespace SchoolAssistant.Services.Data
{
    using System.Collections.Generic;

    public interface ILecturesService
    {
        IEnumerable<T> GetAllByName<T>(string name);
    }
}
