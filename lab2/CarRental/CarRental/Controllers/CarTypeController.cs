using CarRental.Models;
using CarRental.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.Controllers
{
    public class CarTypeController : Controller
    {
        private CarTypeService _carTypeService;
        
        public ActionResult ListCarType()
        {
            _carTypeService = new CarTypeService();
            var model = _carTypeService.GetCarTypes();
            return View(model);
        }

        public ActionResult AddCarType()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCarType(CarTypeModel model)
        {
            _carTypeService = new CarTypeService();
            _carTypeService.AddCarType(model);

            return RedirectToAction("ListCarType");
        }

        public ActionResult UpdateCarType(int id)
        {
            _carTypeService = new CarTypeService();
            var model = _carTypeService.GetCarType(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateCarType(CarTypeModel model)
        {
            _carTypeService = new CarTypeService();
            _carTypeService.UpdateCarType(model);
            return RedirectToAction("ListCarType");
        }

        public ActionResult DeleteCarType(int id)
        {
            _carTypeService = new CarTypeService();
            _carTypeService.DeleteCarType(id);
            return RedirectToAction("ListCarType");
        }
    }
}