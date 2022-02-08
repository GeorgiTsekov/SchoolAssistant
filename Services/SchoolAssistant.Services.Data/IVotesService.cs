namespace SchoolAssistant.Services.Data
{
    using System.Threading.Tasks;

    public interface IVotesService
    {
        Task SetVoteAsync(int courseId, string userId, byte value);

        double GetAverageVotes(int courseId);
    }
}
