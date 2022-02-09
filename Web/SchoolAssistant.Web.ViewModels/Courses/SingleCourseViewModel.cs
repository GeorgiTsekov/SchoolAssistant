namespace SchoolAssistant.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using SchoolAssistant.Data.Models;
    using SchoolAssistant.Services.Mapping;

    public class SingleCourseViewModel : IMapFrom<Course>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string DepartmentName { get; set; }

        public string Description { get; set; }

        public string CreatedByUserUserName { get; set; }

        public double AverageVote { get; set; }

        public DateTime CreatedOn { get; set; }

        public IEnumerable<SingleLectureViewModel> Lectures { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Course, SingleCourseViewModel>()
                .ForMember(x => x.AverageVote, opt =>
                    opt.MapFrom(x => x.Votes.Count() == 0 ? 0 : x.Votes.Average(v => v.Value)));
        }
    }
}
