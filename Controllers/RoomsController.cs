using System.Linq;
using CodesByFola.Tools;

namespace BlueHaisAnisHotelManagement.Controllers
{
    public class RoomsController : Controller
    {
        
        public IActionResult Index(string sortExpression="", string SearchText ="",int pg = 1,int  pageSize = 5) // read method of the crud operarion. It lists all data from the units table.
        {
            SortModel sortModel = new SortModel();

            sortModel.AddColumn("name");
            sortModel.AddColumn("description");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;

            ViewBag.SearchText = SearchText;

            PaginatedList<RoomUnits> RoomUnits = _roomUnitRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText,pg, pageSize); // _context.RoomUnits.ToList();

           // int totRecs = ((PaginatedList<RoomUnits>)RoomUnits).TotalRecords;
                 

            var pager = new PageModel(RoomUnits.TotalRecords, pg, pageSize);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;

            return View(RoomUnits);
        }
        
        private readonly IRoomUnits _roomUnitRepo; 
        public RoomsController(IRoomUnits roomUnitRepo)
        {
            _roomUnitRepo = roomUnitRepo;
        }

        public IActionResult Create()
        {
            RoomUnits roomUnit = new RoomUnits();
            return View(roomUnit);
        }

        [HttpPost]
        public IActionResult Create(RoomUnits roomUnits)
        {
            try
            {
                roomUnits = _roomUnitRepo.Create(roomUnits);

            }
            catch
            {

            }
            return  RedirectToAction(nameof(Index));
        }
        
        public IActionResult Details(int id)
        {
           RoomUnits roomUnits = _roomUnitRepo.GetUnits(id);
            return View(roomUnits);
        }

        public IActionResult Edit(int id)
        {
            RoomUnits roomUnits = _roomUnitRepo.GetUnits(id);
            return View(roomUnits);
        }
        [HttpPost]
        public IActionResult Edit(RoomUnits roomUnits)
        {
            try
            {

                roomUnits = _roomUnitRepo.Edit(roomUnits);
            }
            catch
            {

            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete (int id)
        {
            RoomUnits roomUnits = _roomUnitRepo.GetUnits(id);
            return View(roomUnits);
        }
        [HttpPost]
        public IActionResult Delete(RoomUnits roomUnits)
        {
            try
            {
                roomUnits = _roomUnitRepo.Delete(roomUnits);

            }
            catch
            {

            }
            return RedirectToAction(nameof(Index));
        }

    }
}
 