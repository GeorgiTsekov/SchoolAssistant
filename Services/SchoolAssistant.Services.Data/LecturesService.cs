namespace SchoolAssistant.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using SchoolAssistant.Data.Common.Repositories;
    using SchoolAssistant.Data.Models;
    using SchoolAssistant.Services.Mapping;

    public class LecturesService : ILecturesService
    {
        private readonly IDeletableEntityRepository<Lecture> lecturesRepository;

        public LecturesService(IDeletableEntityRepository<Lecture> lecturesRepository)
        {
            this.lecturesRepository = lecturesRepository;
        }

        public IEnumerable<T> GetAllByName<T>(string name)
        {
            var lectures = this.lecturesRepository
                .All()
                .Where(x => x.Name.Contains(name))
                .To<T>()
                .ToList();

            return lectures;
        }
    }
}
