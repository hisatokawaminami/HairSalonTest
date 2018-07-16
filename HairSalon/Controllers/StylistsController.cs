using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class StylistsController : Controller
  {
    [HttpGet("/stylists/")]
    public ActionResult Index()
    {
      return View(Stylist.GetAll());
    }
    [HttpGet("/stylists/add")]
    public ActionResult StylistForm()
    {
      return View(Stylist.GetAll());
    }
    [HttpPost("/stylists")]
    public ActionResult CollectInfo(string newstylist, int newphone)
    {
      Stylist newStylist = new Stylist(newstylist, newphone);
      newStylist.Save();
      // List<Item> all = Item.GetAll();
      return RedirectToAction("Index");
    }

    [HttpGet("/stylists/{id}/clients")]
    public ActionResult List(int id)
    {
      Stylist thisStylist = Stylist.Find(id);

      List<Client> allClients = thisStylist.GetClients();
      return View(thisStylist);
    }

  }
}
