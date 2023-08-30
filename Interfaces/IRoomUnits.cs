 
using CodesByFola.Tools;

namespace BlueHaisAnisHotelManagement.Interfaces
{
    public interface IRoomUnits
    {
        PaginatedList<RoomUnits> GetItems(string SortProperty, SortOrder sortOrder, string  SearchText = "", int pageIndex = 1, int pageSize = 5);
        RoomUnits GetUnits(int id);

        RoomUnits Create(RoomUnits roomUnits);
        RoomUnits Edit(RoomUnits roomUnits);
        RoomUnits Delete(RoomUnits roomUnits);

        //public bool IsRoomUnitNameExist(string name);
        //public bool IsRoomUnitNameExist(string name, int id);

    }
}
