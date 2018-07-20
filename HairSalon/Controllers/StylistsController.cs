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
    public ActionResult CollectInfo(string newstylist, int newphone, int newclientId)
    {
      Stylist newStylist = new Stylist(newstylist, newphone, newclientId);
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
    [HttpGet("/stylists/{id}/delete")]
    public ActionResult Delete(int id)
    {
      Stylist thisStylist = Stylist.Find(id);
      return View(thisStylist);
    }
    [HttpPost("/stylists/{id}/delete")]
    public ActionResult DeleteStylist(int id)
    {
      Stylist thisStylist = Stylist.Find(id);
      thisStylist.Delete();
      return RedirectToAction("Index");
    }

    [HttpGet("/stylists/{id}/edit")]
    public ActionResult Edit(int id)
    {
      Stylist thisStylist = Stylist.Find(id);
      return View(thisStylist);
    }
    [HttpPost("/stylists/{id}/edit")]
    public ActionResult EditName(int id)
    {
      Stylist thisStylist = Stylist.Find(id);
      thisStylist.Edit(Request.Form["newName"]);
      return RedirectToAction("Index");
    }
  }
}
