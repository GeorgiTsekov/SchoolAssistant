namespace SchoolAssistant.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using SchoolAssistant.Data.Common.Repositories;
    using SchoolAssistant.Data.Models;

    public class VotesService : IVotesService
    {
        private readonly IRepository<Vote> votesRepository;

        public VotesService(IRepository<Vote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public double GetAverageVotes(int courseId)
        {
            return this.votesRepository.All()
                .Where(x => x.CourseId == courseId)
                .Average(x => x.Value);
        }

        public async Task SetVoteAsync(int courseId, string userId, byte value)
        {
            var vote = this.votesRepository.All()
                .FirstOrDefault(v => v.CourseId == courseId && v.UserId == userId);

            if (vote == null)
            {
                vote = new Vote
                {
                    CourseId = courseId,
                    UserId = userId,
                };

                await this.votesRepository.AddAsync(vote);
            }

            vote.Value = value;
            await this.votesRepository.SaveChangesAsync();
        }
    }
}
