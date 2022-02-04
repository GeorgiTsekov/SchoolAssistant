namespace SchoolAssistant.Web.ViewModels
{
    using System;

    public class PagingViewModel
    {
        public bool HasPreviousPageNumber => this.PageNumber > 1;

        public int PreviousPageNumber => this.PageNumber - 1;

        public bool HasNextPageNumber => this.PageNumber < this.PagesCount;

        public int NextPageNumber => this.PageNumber + 1;

        public int PagesCount => (int)Math.Ceiling((double)this.CoursesCount / this.ItemsPerPage);

        public int PageNumber { get; set; }

        public int CoursesCount { get; set; }

        public int ItemsPerPage { get; set; }
    }
}
