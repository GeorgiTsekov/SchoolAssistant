namespace SchoolAssistant.Web.ViewModels.Votes
{
    using System.ComponentModel.DataAnnotations;

    public class PostVoteInputModel
    {
        public int CourseId { get; set; }

        [Range(1, 10)]
        public byte Value { get; set; }
    }
}
