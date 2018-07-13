using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HairSalon.Models;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTests : IDisposable
  {
    public void Dispose()
    {
      Stylist.DeleteAll();
    }
    public StylistTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=hisato_kawaminami_test;";
    }
    [TestMethod]
      public void GetName_ReturnsName_String()
      {
        //Arrange
        string name = "Yoko Bono";
        Stylist newStylist = new Stylist(name);

        //Act
        string result = newStylist.GetName();
        // Console.WriteLine(result);
        //Assert
        Assert.AreEqual(name, result);
      }
      [TestMethod]
      public void SetName_SetName_String()
      {
        //Arrange
        string name = "Yoko Bono";
        Stylist newStylist = new Stylist(name);

        //Act
        string updatedName = "Jon Lemon";
        newStylist.SetName(updatedName);
        string result = newStylist.GetName();

        Console.WriteLine(name);
        Console.Write(updatedName);
      }
      [TestMethod]
      public void GetAll_DbStartsEmpty_0()
      {
        //Arrange
        //ACT
        int result = Stylist.GetAll().Count;

        //Assert
        Assert.AreEqual(0, result);
      }
      [TestMethod]
      public void Equals_ReturTrueIfNamesAreTheSame_Stylist()
      {
        //Arrange, Act
        Stylist firstStylist = new Stylist("Yoko Bono");
        Stylist secondStylist = new Stylist("Yoko Bono");

        //Assert
        Assert.AreEqual(firstStylist, secondStylist);
      }
      [TestMethod]
      public void Save_SavesToDatabase_StylistList()
      {
        //Arrange
        Stylist testStylist = new Stylist("Yoko Bono");

        //Act
        testStylist.Save();
        List<Stylist> result = Stylist.GetAll();
        List<Stylist> testList = new List<Stylist>{testStylist};

        //Assert
        CollectionAssert.AreEqual(testList, result);
      }
      [TestMethod]
      public void Save_AssignsIdToObject_Id()
      {
        //Arrange
        Stylist testStylist = new Stylist("Yoko Bono");

        //Act
        testStylist.Save();
        Stylist savedStylist = Stylist.GetAll()[0];

        int result = savedStylist.GetId();
        int testId = testStylist.GetId();

        //Assert
        Assert.AreEqual(testId, result);
      }
  }
}
