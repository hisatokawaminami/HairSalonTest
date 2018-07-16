using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class ClientTests : IDisposable
  {
    public void Dispose()
    {
      Stylist.DeleteAll();
      Client.DeleteAll();
    }
    public ClientTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=hisato_kawaminami_test;";
    }

    [TestMethod]
    public void GetAll_ClientEmptyAtFisrt_0()
    {
      //Arrange, Act
      int result = Client.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }
    [TestMethod]
    public void Equals_ReturnsTrueIfClientsAreTheSame_Client()
    {
      //Arrange, Act
      Client firstClient = new Client("Yoko Bono");
      Client secondClient = new Client("Yoko Bono");

      //Assert
      Assert.AreEqual(firstClient, secondClient);
    }
    [TestMethod]
    public void Save_SavesToDatabase_ClientList()
    {
      //Arrange
      Client testClient = new Client("Yoko Bono");
      testClient.Save();

      //Act
      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client>{testClient};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToClient_Id()
    {
      //Arrange
      Client testClient = new Client("Yoko Bono");
      testClient.Save();

      //Act
      Client savedClient = Client.GetAll()[0];

      int result = savedClient.GetId();
      int testId = testClient.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }
    [TestMethod]
    public void Find_FindsClientInDatabase_Client()
    {
      //Arrange
      Client testClient = new Client("Yoko Bono");
      testClient.Save();

      //Act
      Client foundClient = Client.Find(testClient.GetId());

      //Assert
      Assert.AreEqual(testClient, foundClient);
    }

    // [TestMethod]
    // public void GetItems_RetrievesAllStylistsWithClient_StylistList()
    // {
    //   Client testClient = new Client("Household chores");
    //   testClient.Save();
    //
    //   Stylist firstStylist = new Stylist("Mow the lawn", 1, testClient.GetId());
    //   firstStylist.Save();
    //   Stylist secondStylist = new Stylist("Do the dishes", 1, testClient.GetId());
    //   secondStylist.Save();
    //
    //   List<Stylist> testStylistList = new List<Stylist> {firstStylist, secondStylist};
    //   List<Stylist> resultStylistList = testClient.GetStylists();
    //
    //
    //   CollectionAssert.AreEqual(testStylistList, resultStylistList);
    // }
    //

  }
}
