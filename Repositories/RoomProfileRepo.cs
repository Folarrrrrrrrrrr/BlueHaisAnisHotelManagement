
using CodesByFola.Tools;
using Microsoft.CodeAnalysis.CSharp.Syntax;



namespace BlueHaisAnisHotelManagement.Repositories
{
    public class RoomProfileRepo : IRoomProfiles
    {
        private readonly HotelContext _context;
        public RoomProfileRepo(HotelContext context)
        {
            _context = context;
        }

        public RoomProfile Create(RoomProfile roomProfile)
        {
            _context.RoomProfiles.Add(roomProfile);
            _context.SaveChanges();
            return roomProfile;
        }

        public RoomProfile Delete(RoomProfile roomProfile)
        {
            _context.RoomProfiles.Attach(roomProfile);
            _context.Entry(roomProfile).State = EntityState.Deleted;
            _context.SaveChanges();
            return roomProfile;
        }

        public RoomProfile Edit(RoomProfile roomProfile)
        {
            _context.RoomProfiles.Attach(roomProfile);
            _context.Entry(roomProfile).State = EntityState.Modified;
            _context.SaveChanges();
            return roomProfile;
        }

        private List<RoomProfile> DoSort(List<RoomProfile> items, string SortProperty, SortOrder sortOrder)
        {
            if (SortProperty.ToLower() == "name")
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(n => n.Name).ToList();
                else
                    items = items.OrderByDescending(n => n.Name).ToList();
            }
            else
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(d => d.Description).ToList();
                else
                    items = items.OrderByDescending(d => d.Description).ToList();
            }
            return items;
        }
        public PaginatedList<RoomProfile> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<RoomProfile> items = _context.RoomProfiles.ToList();  //fectching roomUnits list from database
            if (SearchText != "" && SearchText != null)
            {
                items = _context.RoomProfiles.Where(n => n.Name.Contains(SearchText) || n.Description.Contains(SearchText)).ToList();
            }
            else
                items = _context.RoomProfiles.ToList();

            items = DoSort(items, SortProperty, sortOrder);
            PaginatedList<RoomProfile> retItems = new PaginatedList<RoomProfile>(items, pageIndex, pageSize);


            return retItems;
        }

        public RoomProfile GetUnits(int id)
        {
            RoomProfile items = _context.RoomProfiles.Where(u => u.Id == id).FirstOrDefault();
            return items;
        }

        public bool IsItemExists(string name)
        {
            int ct = _context.RoomProfiles.Where(n => n.Name.ToLower() ==n.Name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public bool IsItemExists(string name, int Id)
        {
            int ct = _context.RoomProfiles.Where(n => n.Name.ToLower() == n.Name.ToLower() && n.Id != Id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public RoomProfile GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsItemExist(string name)
        {
            throw new NotImplementedException();
        }

        public bool IsItemExist(string name, int id)
        {
            throw new NotImplementedException();
        }
    }
}
