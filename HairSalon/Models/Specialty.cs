using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;
using System;

namespace HairSalon.Models
{
  public class Specialty
  {
    private int _id;
    private string _specialty;



    public Specialty (string Specialty, int Id = 0)
    {
      _id = Id;
      _specialty = Specialty;

    }

    public override bool Equals(System.Object otherSpecialty)
    {
      if (!(otherSpecialty is Specialty))
      {
        return false;
      }
      else
      {
        Specialty newSpecialty = (Specialty) otherSpecialty;
        bool idEquality = (this.GetId() == newSpecialty.GetId());
        bool specialtyEquality = (this.GetSpecialty() == newSpecialty.GetSpecialty());
        return (idEquality && specialtyEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetSpecialty().GetHashCode();
    }

    public string GetSpecialty()
    {
      return _specialty;
    }
    public void SetSpecialty(string newSpecialty)
    {
      _specialty = newSpecialty;
    }
    public int GetId()
    {
      return _id;
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
    public static List<Specialty> GetAll()
    {
      List<Specialty> allSpecialtys = new List<Specialty> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistSpecialty = rdr.GetString(1);
        Specialty newSpecialty = new Specialty(stylistSpecialty, stylistId);
        allSpecialtys.Add(newSpecialty);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allSpecialtys;
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists (specialty) VALUES (@SpecialtySpecialty);";

      MySqlParameter specialty = new MySqlParameter();
      specialty.ParameterName = "@SpecialtySpecialty";
      specialty.Value = this._specialty;
      cmd.Parameters.Add(specialty);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static Specialty Find(int id)
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
      string stylistSpecialty = "";

      while (rdr.Read())
      {
        stylistId = rdr.GetInt32(0);
        stylistSpecialty = rdr.GetString(1);
      }

      Specialty foundSpecialty = new Specialty(stylistSpecialty, stylistId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundSpecialty;

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
        string clientSpecialty = rdr.GetString(1);
        int clientSpecialtyId = rdr.GetInt32(2);

        Client newClient = new Client(clientSpecialty, clientSpecialtyId, clientId);
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
      cmd.CommandText = @"DELETE FROM stylists WHERE id = @SpecialtyId; DELETE FROM clients_stylists WHERE stylist_id = @SpecialtyId;";

      MySqlParameter stylistIdParameter = new MySqlParameter();
      stylistIdParameter.ParameterName = "@SpecialtyId";
      stylistIdParameter.Value = this.GetId();
      cmd.Parameters.Add(stylistIdParameter);

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public void Edit(string newSpecialty)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE stylists SET specialty = @newSpecialty WHERE id =@SpecialtyId;";

      MySqlParameter stylistIdParameter = new MySqlParameter();
      stylistIdParameter.ParameterName = "@SpecialtyId";
      stylistIdParameter.Value = _id;
      cmd.Parameters.Add(stylistIdParameter);

      MySqlParameter specialty = new MySqlParameter();
      specialty.ParameterName = "@newSpecialty";
      specialty.Value = newSpecialty;
      cmd.Parameters.Add(specialty);

      cmd.ExecuteNonQuery();
      _specialty = newSpecialty;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
