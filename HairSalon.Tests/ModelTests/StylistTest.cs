using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HairSalon.Models;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTests
  {
    // public void Dispose()
    // {
    //   Stylist.DeleteAll();
    // }
    public StylistTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=hisato_kawaminami_test;";
    }
    [TestMethod]
      public void GetName_ReturnsName_String()
      {
        //Arrange
        string name = "Yoko Ono";
        Stylist newStylist = new Stylist(name);

        //Act
        string result = newStylist.GetName();
        Console.WriteLine(result);
        //Assert
        Assert.AreEqual(name, result);
      }
  }
}
