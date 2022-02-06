namespace SchoolAssistant.Data.Models
{
    using System;

    using SchoolAssistant.Data.Common.Models;

    public class Presentation : BaseDeletableModel<string>
    {
        public Presentation()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string LectureId { get; set; }

        public virtual Lecture Lecture { get; set; }

        public string RemotePresentationUrl { get; set; }

        public string CreatedByUserId { get; set; }

        public ApplicationUser CreatedByUser { get; set; }

        public string Extension { get; set; }
    }
}
