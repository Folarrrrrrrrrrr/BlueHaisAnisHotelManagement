
 
using CodesByFola.Tools;

namespace BlueHaisAnisHotelManagement.Interfaces
{
    public interface IRoomProfiles
    {
        PaginatedList<RoomProfile> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        RoomProfile GetItem(int id);

        RoomProfile Create(RoomProfile roomUnits);
        RoomProfile Edit(RoomProfile roomUnits);
        RoomProfile Delete(RoomProfile roomUnits);

        public bool IsItemExist(string name);
        public bool IsItemExist(string name, int id);

    }
}


