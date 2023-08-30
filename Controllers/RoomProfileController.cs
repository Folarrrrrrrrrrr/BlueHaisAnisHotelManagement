

using System.Linq;
using BlueHaisAnisHotelManagement.Interfaces;

using BlueHaisAnisHotelManagement.Models;
using CodesByFola.Tools;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace BlueHaisAnisHotelManagement.Controllers
{
    public class RoomProfileController : Controller
    {
        private readonly IRoomProfiles _Repo;
        public RoomProfileController(IRoomProfiles repo)
        {
            _Repo = repo;
        }


        public IActionResult Index(string sortExpression = "", string SearchText = "", int pg = 1, int pageSize = 5) // read method of the crud operarion. It lists all data from the units table.
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("name");
            sortModel.AddColumn("description");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;

            ViewBag.SearchText = SearchText;

            PaginatedList<RoomProfile> itens = _Repo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize); // _context.RoomUnits.ToList();

            // int totRecs = ((PaginatedList<RoomUnits>)RoomUnits).TotalRecords;


            var pager = new PageModel(itens.TotalRecords, pg, pageSize);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;

            return View(itens);
        }

       

        public IActionResult Create()
        {
            RoomProfile item = new RoomProfile();
            return View(item);
        }

        [HttpPost]
        public IActionResult Create(RoomProfile item)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                if (item.Description.Length < 4 || item.Description == null)
                    errMessage = "Description Must be atleat 4 Characters";
                if (_Repo.IsItemExist(item.Name) == true)
                    errMessage =errMessage + " " + "Namme" + item.Name + "Exists Already";

                if (errMessage == "")
                {
                    item = _Repo.Create(item);
                    bolret = true; 
                }
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }
            if (bolret == false)
            {
                TempData["ErrorMessge"] = errMessage;
                ModelState.AddModelError("", errMessage);
                return View(item);
            }
            else
            {
                TempData["UsccesMessage"] = "" + item.Name + " Created Successfully";
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Details(int id) //Read
        {
            RoomProfile item = _Repo.GetItem(id);
            return View(item);
        }

        public IActionResult Edit(int id)
        {
            RoomProfile item = _Repo.GetItem(id);
            TempData.Keep();
            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(RoomProfile item)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                if (item.Description.Length < 4 || item.Description == null)
                    errMessage = "Description Must be atleast 4 Characters";
                if (_Repo.IsItemExist(item.Name, item.Id) == true)
                    errMessage = errMessage + "item Name" + item.Name + " Already Exist";

                if (errMessage == "")
                {
                    item = _Repo.Edit(item);
                    TempData["SuccessMessage"] = item.Name + ", saved Successfully";
                    bolret = true;
                }
            }
            catch(Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            int currentPage = 1;
            if (TempData["CurrentPage"] != null)
                currentPage = (int)TempData["CurrentPage"];

            if(bolret == false)
            {
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("" , errMessage);
                return View();
            }

            return RedirectToAction(nameof(Index), new {pg = currentPage});
        }
        public IActionResult Delete(int id)
        {
            RoomProfile item = _Repo.GetItem(id);
            TempData.Keep(); 
            return View(item);
        }
        [HttpPost]
        public IActionResult Delete(RoomProfile item)
        {
            try
            {
                item = _Repo.Delete(item);

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage); 
                return View(item);
            }
            int currentPage = 1;
            if (TempData["CurrentPage"] != null)
                currentPage = (int)TempData["CurrentPage"];

            TempData["SuccessMessage"] = item.Name + " Deleted Suiccessfully";
            return RedirectToAction(nameof(Index) , new {pg = currentPage });
        }

    }
}

