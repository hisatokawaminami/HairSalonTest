using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class ClientsController : Controller
  {
    [HttpGet("/clients")]
    public ActionResult Index()
    {
    return View(Client.GetAll());
    }
    [HttpPost("/clients")]
    public ActionResult CollectClient()
    {
    Client newClient = new Client(Request.Form["new-client"]);
    newClient.Save();
    List<Client> allClients = Client.GetAll();
    return RedirectToAction("Index");
    }
    [HttpPost("/clients/{id}/items")]
    public ActionResult List(int id)
    {
      Client thisClient = Client.Find(id);

      List<Item> allItems = thisCategory.GetItems();
      return View(this);
    }


    [HttpGet("/clients/")]
    public ActionResult ClientForm()
    {
      return View(Client.GetAll());
    }

  }
}
