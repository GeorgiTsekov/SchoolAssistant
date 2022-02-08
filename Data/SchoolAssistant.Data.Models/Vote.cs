namespace SchoolAssistant.Data.Models
{
    using SchoolAssistant.Data.Common.Models;

    public class Vote : BaseModel<int>
    {
        public virtual Course Course { get; set; }

        public int CourseId { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public byte Value { get; set; }
    }
}
