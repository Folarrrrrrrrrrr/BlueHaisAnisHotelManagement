using CodesByFola.Tools;
using Microsoft.CodeAnalysis.CSharp.Syntax;



namespace BlueHaisAnisHotelManagement.Repositories
{
    public class RoomUnitsRepository : IRoomUnits
    {
        private readonly HotelContext _context;
        public RoomUnitsRepository(HotelContext context)
        {
            _context = context;
        }

        public RoomUnits Create(RoomUnits roomUnits)
        {
            _context.RoomUnits.Add(roomUnits);
            _context.SaveChanges();
            return roomUnits; 
        }

        public RoomUnits Delete(RoomUnits roomUnits)
        {
            _context.RoomUnits.Attach(roomUnits);
            _context.Entry(roomUnits).State = EntityState.Deleted;
            _context.SaveChanges();
            return roomUnits;   
        }

        public RoomUnits Edit(RoomUnits roomUnits)
        {
            _context.RoomUnits.Attach(roomUnits);
            _context.Entry(roomUnits).State = EntityState.Modified;
            _context.SaveChanges();
            return roomUnits;
        }

        private  List<RoomUnits> DoSort(List<RoomUnits> roomUnits, string SortProperty, SortOrder  sortOrder)
        {
            if (SortProperty.ToLower() == "name")
            {
                if (sortOrder == SortOrder.Ascending)
                    roomUnits = roomUnits.OrderBy(n => n.Name).ToList();
                else
                    roomUnits = roomUnits.OrderByDescending(n => n.Name).ToList();
            }
            else
            {
                if (sortOrder == SortOrder.Ascending)
                    roomUnits = roomUnits.OrderBy(d => d.Description).ToList();
                else
                    roomUnits = roomUnits.OrderByDescending(d => d.Description).ToList();
            }
            return roomUnits;
        }
        public PaginatedList<RoomUnits> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "",int pageIndex = 1, int pageSize = 5)  
        {
            List<RoomUnits> roomUnits = _context.RoomUnits.ToList();  //fectching roomUnits list from database
            if (SearchText != "" && SearchText != null)
            {
                roomUnits = _context.RoomUnits.Where(n => n.Name.Contains(SearchText) || n.Description.Contains(SearchText)).ToList();    
            }
            else
                roomUnits = _context.RoomUnits.ToList();

            roomUnits = DoSort( roomUnits,SortProperty, sortOrder);
            PaginatedList<RoomUnits> retRoomUnits = new PaginatedList<RoomUnits>(roomUnits, pageIndex, pageSize);


            return retRoomUnits;
        } 

        public RoomUnits GetUnits(int id)
        {
            RoomUnits roomUnits = _context.RoomUnits.Where(u => u.Id == id).FirstOrDefault();
            return roomUnits;
        }
        
    }
}
 