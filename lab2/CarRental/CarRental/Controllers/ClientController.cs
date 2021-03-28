using CarRental.Models;
using CarRental.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.Controllers
{
    public class ClientController : Controller
    {
        private ClientService _clientService;

        public ActionResult ListClient()
        {
            _clientService = new ClientService();
            var model = _clientService.GetClients();
            return View(model);
        }

        public ActionResult AddClient()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddClient(ClientModel model)
        {
            _clientService = new ClientService();
            _clientService.AddClient(model);

            return RedirectToAction("ListClient");
        }

        public ActionResult UpdateClient(int id)
        {
            _clientService = new ClientService();
            var model = _clientService.GetClient(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateClient(ClientModel model)
        {
            _clientService = new ClientService();
            _clientService.UpdateClient(model);
            return RedirectToAction("ListClient");
        }

        public ActionResult DeleteClient(int id)
        {
            _clientService = new ClientService();
            _clientService.DeleteClient(id);
            return RedirectToAction("ListClient");
        }
    }
}