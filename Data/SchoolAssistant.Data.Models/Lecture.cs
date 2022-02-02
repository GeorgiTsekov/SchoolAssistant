namespace SchoolAssistant.Data.Models
{
    using System.Collections.Generic;

    using SchoolAssistant.Data.Common.Models;

    public class Lecture : BaseDeletableModel<int>
    {
        public Lecture()
        {
            this.Presentations = new HashSet<Presentation>();
        }

        public string Name { get; set; }

        public string VideoUrl { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }

        public virtual ICollection<Presentation> Presentations { get; set; }
    }
}
