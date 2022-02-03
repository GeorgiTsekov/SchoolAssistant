namespace SchoolAssistant.Services.Data
{
    using SchoolAssistant.Web.ViewModels.Home;

    public interface IGetCountsService
    {
        IndexViewModel GetCounts();
    }
}
