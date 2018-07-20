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
        int phone = 0;
        int clientId = 1;
        Stylist newStylist = new Stylist(name, phone, clientId);

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
        int phone = 0;
        int clientId = 1;

        Stylist newStylist = new Stylist(name, phone, clientId);

        //Act
        string updatedName = "Jon Lemon";
        newStylist.SetName(updatedName);
        string result = newStylist.GetName();


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
        Stylist firstStylist = new Stylist("Yoko Bono", 0, 1);
        Stylist secondStylist = new Stylist("Yoko Bono", 0, 1);


        //Assert
        Assert.AreEqual(firstStylist, secondStylist);
      }
      [TestMethod]
      public void Save_SavesToDatabase_StylistList()
      {
        //Arrange
        Stylist testStylist = new Stylist("Yoko Bono", 0, 1);

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
        Stylist testStylist = new Stylist("Yoko Bono", 0, 1);

        //Act
        testStylist.Save();
        Stylist savedStylist = Stylist.GetAll()[0];

        int result = savedStylist.GetId();
        int testId = testStylist.GetId();

        //Assert
        Assert.AreEqual(testId, result);
      }
      [TestMethod]
      public void Find_FindsItemInDatabase_Item()
      {
        //Arrange
        Stylist testStylist = new Stylist("Yoko Bono", 0, 1);
        testStylist.Save();

        //Act
        Stylist foundStylist = Stylist.Find(testStylist.GetId());

        //Assert
        Assert.AreEqual(testStylist,foundStylist);
      }

      [TestMethod]
      public void Delete_A_Specific_Stylist()
      {
        //Arrange
        Stylist testStylist = new Stylist("Yoko", 123, 1);
        testStylist.Save();
        Stylist testStylist2 = new Stylist("Jon", 45, 2);
        testStylist2.Save();

        //Act
        testStylist.Delete();
        List<Stylist> expectedList = new List<Stylist>{testStylist2};

        //Assert
        List<Stylist> outputList = Stylist.GetAll();
        Assert.IsTrue(outputList.Count ==1);
        CollectionAssert.AreEqual(expectedList, outputList);
      }
      [TestMethod]
      public void Edit_EditStylistNameInDatabase_String()
      {
        //Arrange
        string testName = "Yoko";
        int phone = 000;
        int clientId = 1;

        Stylist testStylist = new Stylist(testName, phone, clientId);
        testStylist.Save();
        string editName = "John";

        //Act
        testStylist.Edit(editName);
        string result = Stylist.Find(testStylist.GetId()).GetName();

        //Assert
        Assert.AreEqual(editName, result);

      }
  }
}
