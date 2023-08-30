namespace BlueHaisAnisHotelManagement.Models
{
    public class PaginatedList<T> : List<T>
    {
        public int TotalRecords { get; private set; }

        public PaginatedList(List<T> source,   int pageIndex, int pageSize) 
        {
            TotalRecords = source.Count;

            // making the paging bars/counts work
            var items  = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();       // filling the room unit list with the records of the selected page  only 
           
            this.AddRange(items);
        }
    }
}
