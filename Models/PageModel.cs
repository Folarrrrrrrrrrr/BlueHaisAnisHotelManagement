using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Drawing.Printing;

namespace BlueHaisAnisHotelManagement.Models
{
    public class PageModel
    {
      

        //note that when the set method is made private 
        //it behaves like a read only as done below.
        public int TotalItems { get; private set; } 
        public int CurrentPage { get; private set;}
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
        public int StartRecord { get; private set; }
        public int EndRecord { get; private set; }

        //Public Properties
        public string Action { get; set; } = "Index";
        public string SearchText { get; set; }
        public string SortExpression { get; set; }
    
        public PageModel(int totalItems, int currentPage, int pageSize=5) 
        {
            this.CurrentPage = currentPage;
            this.TotalItems = totalItems;
            this.PageSize= pageSize;

            int totalPages = (int)Math.Ceiling((decimal)totalItems/(decimal)PageSize);

            TotalPages = totalPages;
            int startPage = CurrentPage - 5;
            int endPage = currentPage + 4;

            if (startPage <= 0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                    startPage = endPage - 9;
            }
            StartRecord = (CurrentPage - 1) * pageSize + 1;
            EndRecord = StartRecord - 1 + pageSize;
            if (EndRecord > TotalItems)
                EndRecord = TotalItems;
            if (TotalItems == 0)
            {
                StartPage= 0;
                StartRecord = 0;
                EndRecord = 0;
                CurrentPage= 0;
            }
            else
            {
                StartPage = startPage;
                EndPage = endPage;
            }
        }


        public List<SelectListItem> GetPagesSizes()
        {

            var pageSizes = new List<SelectListItem>();

            for (int lp = 5; lp <= 50; lp+=5)
            {
                if (lp == this.PageSize)
                {
                    pageSizes.Add(new SelectListItem(lp.ToString(), lp.ToString(), true));
                }
                else
                    pageSizes.Add(new SelectListItem(lp.ToString(), lp.ToString()));
            }
            return pageSizes;
        }
    }
}
