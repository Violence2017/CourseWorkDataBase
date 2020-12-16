using System;

namespace CourseWork.ViewModels
{
    public class PageViewModel
    {
        public PageViewModel(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);
        }

        public int PageNumber { get; }
        private int TotalPages { get; }

        public bool HasPreviousPage => PageNumber > 1;

        public int PreviousPageNumber => PageNumber - 1;

        public bool HasSecondPreviousPage => PageNumber - 1 > 1;

        public int SecondPreviousPageNumber => PageNumber - 2;

        public bool HasNextPage => PageNumber < TotalPages;

        public int NextPageNumber => PageNumber + 1;

        public bool HasSecondNextPage => PageNumber + 1 < TotalPages;

        public int SecondNextPageNumber => PageNumber + 2;
    }
}