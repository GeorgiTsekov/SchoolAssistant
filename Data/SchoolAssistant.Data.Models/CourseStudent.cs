namespace SchoolAssistant.Data.Models
{
    using SchoolAssistant.Data.Common.Models;

    public class CourseStudent : BaseDeletableModel<int>
    {
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public string StudentId { get; set; }

        public virtual ApplicationUser Student { get; set; }
    }
}
