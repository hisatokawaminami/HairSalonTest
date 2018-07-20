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
    private int _phone;
    private int _clientId;



    public Stylist (string Name, int Phone, int ClientId, int Id = 0)
    {
      _id = Id;
      _name = Name;
      _phone = Phone;
      _clientId = ClientId;
    }

    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool idEquality = (this.GetId() == newStylist.GetId());
        bool nameEquality = (this.GetName() == newStylist.GetName());
        bool clientIdEquality = (this.GetClientId() == newStylist.GetClientId());
        return (idEquality && nameEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }

    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }
    public int GetId()
    {
      return _id;
    }
    public int GetPhone()
    {
      return _phone;
    }
    public int GetClientId()
    {
      return _clientId;
    }
    public void SetClientId(int newClientId)
    {
      _clientId = newClientId;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylists = new List<Stylist> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        int stylistPhone = rdr.GetInt32(2);
        int stylistClientId = rdr.GetInt32(3);
        Stylist newStylist = new Stylist(stylistName, stylistPhone, stylistClientId, stylistId);
        allStylists.Add(newStylist);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allStylists;
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists (name, phone, clientId) VALUES (@StylistName, @StylistPhone, @StylistClientId);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@StylistName";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      MySqlParameter phone = new MySqlParameter();
      phone.ParameterName = "@StylistPhone";
      phone.Value = this._phone;
      cmd.Parameters.Add(phone);

      MySqlParameter clientId = new MySqlParameter();
      clientId.ParameterName = "@StylistClientId";
      clientId.Value = this._clientId;
      cmd.Parameters.Add(clientId);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static Stylist Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText  = @"SELECT * FROM stylists WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int stylistId = 0;
      string stylistName = "";
      int stylistPhone = 0;
      int stylistClientId = 0;

      while (rdr.Read())
      {
        stylistId = rdr.GetInt32(0);
        stylistName = rdr.GetString(1);
        stylistPhone = rdr.GetInt32(2);
        stylistClientId = rdr.GetInt32(3);
      }

      Stylist foundStylist = new Stylist(stylistName, stylistPhone, stylistClientId, stylistId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundStylist;

    }
    public List<Client> GetClients()
    {
      List<Client> allCategoryClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = @stylist_id;";

      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@stylist_id";
      stylistId.Value = this._id;
      cmd.Parameters.Add(stylistId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        int clientStylistId = rdr.GetInt32(2);

        Client newClient = new Client(clientName, clientStylistId, clientId);
        allCategoryClients.Add(newClient);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCategoryClients;
    }
    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists WHERE id = @StylistId; DELETE FROM clients_stylists WHERE stylist_id = @StylistId;";

      MySqlParameter stylistIdParameter = new MySqlParameter();
      stylistIdParameter.ParameterName = "@StylistId";
      stylistIdParameter.Value = this.GetId();
      cmd.Parameters.Add(stylistIdParameter);

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public void Edit(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE stylists SET name = @newName WHERE id =@StylistId;";

      MySqlParameter stylistIdParameter = new MySqlParameter();
      stylistIdParameter.ParameterName = "@StylistId";
      stylistIdParameter.Value = _id;
      cmd.Parameters.Add(stylistIdParameter);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newName";
      name.Value = newName;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      _name = newName;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
