namespace SchoolAssistant.Web.ViewModels.Courses
{
    using System.Linq;

    using AutoMapper;
    using SchoolAssistant.Data.Models;
    using SchoolAssistant.Services.Mapping;

    public class LecturesViewModel : IMapFrom<Lecture>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string VideoUrl { get; set; }

        public string PresentationUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Lecture, LecturesViewModel>()
                .ForMember(x => x.PresentationUrl, opt =>
                    opt.MapFrom(x =>
                        x.Presentations.FirstOrDefault().RemotePresentationUrl != null ?
                        x.Presentations.FirstOrDefault().RemotePresentationUrl :
                        "/presentations/lectures/courses/" + x.Presentations.FirstOrDefault().Id + "." + x.Presentations.FirstOrDefault().Extension));
        }
    }
}
