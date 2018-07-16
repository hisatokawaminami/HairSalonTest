using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class ClientsController : Controller
  {
    [HttpPost("/clients")]
    public ActionResult CollectInfo(string newclient, int stylist)
    {
      Client newClient = new Client(newclient, stylist);
      newClient.Save();
      // List<Client> all = Client.GetAll();
      return RedirectToAction("Index");
    }

    [HttpGet("/clients")]
    public ActionResult Index()
    {
      // List<Client> all = Client.GetAll();
      return View(Client.GetAll());
    }

    [HttpGet("/clients/add")]
    public ActionResult ClientForm()
    {
      return View(Stylist.GetAll());
    }

    







    // [HttpGet("/clients")]
    // public ActionResult Index()
    // {
    // return View(Client.GetAll());
    // }
    //
    // [HttpPost("/clients/add")]
    // public ActionResult CollectClient(string newclient, int stylist)
    // {
    // Client newClient = new Client(newclient, stylist);
    // newClient.Save();
    // List<Client> allClients = Client.GetAll();
    // return RedirectToAction("Index");
    // }
    // [HttpPost("/clients/{id}/items")]
    // public ActionResult List(int id)
    // {
    //   Client thisClient = Client.Find(id);
    //
    //   List<Stylist> allItems = thisClient.GetItems();
    //   return View(this);
    // }
    //
    //
    // [HttpGet("/clients/add")]
    // public ActionResult ClientForm()
    // {
    //   return View(Client.GetAll());
    // }
  }
}
