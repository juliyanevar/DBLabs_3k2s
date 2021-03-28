using CarRental.Models;
using CarRental.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.Controllers
{
    public class CarController : Controller
    {
        private CarService _carService;
        private BrandService _brandService;
        private CarTypeService _carTypeService;

        public ActionResult ListCar()
        {
            _carService = new CarService();
            var model = _carService.GetCars();
            return View(model);
        }

        public ActionResult AddCar()
        {
            _brandService = new BrandService();
            _carTypeService = new CarTypeService();
            SelectList brands = new SelectList(_brandService.GetBrands(), "Id", "Brand");
            SelectList carTypes = new SelectList(_carTypeService.GetCarTypes(), "Id", "CarType");
            ViewBag.Brands = brands;
            ViewBag.CarTypes = carTypes;
            return View();
        }

        [HttpPost]
        public ActionResult AddCar(CarModel model)
        {
            _carService = new CarService();
            _carService.AddCar(model);

            return RedirectToAction("ListCar");
        }

        public ActionResult UpdateCar(int id)
        {
            _carService = new CarService();
            var model = _carService.GetCar(id);
            if (model != null)
            {
                _brandService = new BrandService();
                _carTypeService = new CarTypeService();
                SelectList brands = new SelectList(_brandService.GetBrands(), "Id", "Brand");
                SelectList carTypes = new SelectList(_carTypeService.GetCarTypes(), "Id", "CarType");
                ViewBag.Brands = brands;
                ViewBag.CarTypes = carTypes;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateCar(CarModel model)
        {
            _carService = new CarService();
            _carService.UpdateCar(model);
            return RedirectToAction("ListCar");
        }

        public ActionResult DeleteCar(int id)
        {
            _carService = new CarService();
            _carService.DeleteCar(id);
            return RedirectToAction("ListCar");
        }
    }
}