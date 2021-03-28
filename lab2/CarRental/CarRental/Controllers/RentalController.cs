using CarRental.Dto;
using CarRental.Models;
using CarRental.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.Controllers
{
    public class RentalController : Controller
    {
        private RentalService _rentalService;
        private ClientService _clientService;
        private CarService _carService;

        public ActionResult ListRental()
        {
            _rentalService = new RentalService();
            var model = _rentalService.GetRentals();
            return View(model);
        }

        public ActionResult AddRental()
        {
            _clientService = new ClientService();
            _carService = new CarService();
            SelectList clients = new SelectList(_clientService.GetClients(), "Id", "Name");
            SelectList cars = new SelectList(_carService.GetCars(), "Id", "GovernmentPlate");
            ViewBag.Clients = clients;
            ViewBag.Cars = cars;
            return View();
        }

        [HttpPost]
        public ActionResult AddRental(RentalModel model)
        {
            _rentalService = new RentalService();
            _rentalService.AddRental(model);

            return RedirectToAction("ListRental");
        }

        public ActionResult UpdateRental(int id)
        {
            _rentalService = new RentalService();
            var model = _rentalService.GetRental(id);
            if (model != null)
            {
                _clientService = new ClientService();
                _carService = new CarService();
                SelectList clients = new SelectList(_clientService.GetClients(), "Id", "Name");
                SelectList cars = new SelectList(_carService.GetCars(), "Id", "GovernmentPlate", "Brand");
                ViewBag.Clients = clients;
                ViewBag.Cars = cars;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateRental(RentalModel model)
        {
            _rentalService = new RentalService();
            _rentalService.UpdateRental(model);
            return RedirectToAction("ListRental");
        }

        public ActionResult DeleteRental(int id)
        {
            _rentalService = new RentalService();
            _rentalService.DeleteRental(id);
            return RedirectToAction("ListRental");
        }

        public ActionResult ListRentalPeriod(ServicesByDateDto model)
        {
            _rentalService = new RentalService();
            return View("ListRental", _rentalService.GetRentalsOverPeriod(model.FirstDay, model.LastDay));
        }

        public ActionResult GetRentalOverPeriod()
        {
            
            return View(new ServicesByDateDto());
        }

        [HttpPost]
        public ActionResult GetRentalOverPeriod(ServicesByDateDto model)
        {
            return RedirectToAction("ListRentalPeriod", model) ;
        }
    }
}