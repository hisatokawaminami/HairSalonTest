using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;
using System;

namespace HairSalon.Models
{
  public class Stylist
  {
    private int _id;
    private string _name;

    public Stylist (string Name, int Id = 0)
    {
      _id = Id;
      _name = Name;
    }
    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }
  }
}
