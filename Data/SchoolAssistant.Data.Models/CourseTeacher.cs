namespace SchoolAssistant.Data.Models
{
    using SchoolAssistant.Data.Common.Models;

    public class CourseTeacher : BaseDeletableModel<int>
    {
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public string TeacherId { get; set; }

        public virtual ApplicationUser Teacher { get; set; }
    }
}
