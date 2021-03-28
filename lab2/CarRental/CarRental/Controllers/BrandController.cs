using CarRental.Models;
using CarRental.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.Controllers
{
    public class BrandController : Controller
    {
        private BrandService _brandService;
        // GET: Brand
        public ActionResult ListBrand()
        {
            _brandService = new BrandService();
            var model = _brandService.GetBrands();
            return View(model);
        }

        public ActionResult AddBrand()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBrand(BrandModel model)
        {
            _brandService = new BrandService();
            _brandService.AddBrand(model);

            return RedirectToAction("ListBrand");
        }

        public ActionResult UpdateBrand(int id)
        {
            _brandService = new BrandService();
            var model = _brandService.GetBrand(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateBrand(BrandModel model)
        {
            _brandService = new BrandService();
            _brandService.UpdateBrand(model);
            return RedirectToAction("ListBrand");
        }

        public ActionResult DeleteBrand(int id)
        {
            _brandService = new BrandService();
            _brandService.DeleteBrand(id);
            return RedirectToAction("ListBrand");
        }
    }
}