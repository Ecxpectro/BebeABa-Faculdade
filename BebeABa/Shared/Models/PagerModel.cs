using System;

namespace Shared.Models
{
    public class PagerModel
    {
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }

        public PagerModel() { }

        public PagerModel(int totalItems, int page, int pageSize = 10)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            int currentPage = page;

            int starPage = currentPage - 3;
            int endPage = currentPage + 3;

            if (starPage <= 0)
            {
                endPage = endPage - (starPage - 1);
                starPage = 1;
            }

            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 7)
                {
                    starPage = endPage - 6;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = starPage;
            EndPage = endPage;
        }
    }
}
