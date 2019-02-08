using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CapstoneTests
{
    [TestClass]
    public class UnitTest1
    {
        //CollectionAssert
        //.AllItemsAreNotNull() - Looks at each item in actual collection for not null
        //.AllItemsAreUnique() - Checks for uniqueness among actual collection
        //.AreEqual() - Checks to see if two collections are equal (same order and quantity)
        //.AreEquilavent() - Checks to see if two collections have same element in same quantity, any order
        //.AreNotEqual() - Opposite of AreEqual
        //.AreNotEquilavent() - Opposite or AreEqualivent
        //.Contains() - Checks to see if collection contains a value/object

        //Assert
        //.AreEqual() - compares expected and actual value for equality
        //.AreSame() - verifies two object variables refer to same object
        //.AreNotSame() - verifies two object variables refer to different objects
        //.Fail() - fails without checking conditions
        //.IsFalse()
        //.IsTrue()
        //.IsNotNull()
        //.IsNull()

        [TestMethod]
        public void VendingMachineTestCreate()
        {
            VendingMachine vmc = new VendingMachine();
            Assert.IsNotNull(vmc);
        }

        [TestMethod]
        public void ReadFileTestCorrect()
        {
            VendingMachine vm = new VendingMachine();
            bool test = vm.ReadFile();
            Assert.AreEqual(true, test);

        }

        [TestMethod]
        public void ReadFileTestIncorrect()
        {
            VendingMachine dm = new VendingMachine();
            dm.filePath = @"C:\DoesntExist";
            bool test = dm.ReadFile();
            Assert.AreEqual(false, test);

        }

        [TestMethod]
        public void ToArrayTest()
        {

        }

        [TestMethod]
        public void SelectProductForPurchaseTest()
        {

        }//string slot

        [TestMethod]
        public void FundsSufficientTest()
        {

        }//string slot

        [TestMethod]
        public void InStockTestZero()
        {
            VendingMachine vm = new VendingMachine();
            vm.ReadFile();
            vm.UserBalance = 500.00M;
            vm.SelectProductForPurchase("A1");
            vm.SelectProductForPurchase("A1");
            vm.SelectProductForPurchase("A1");
            vm.SelectProductForPurchase("A1");
            vm.SelectProductForPurchase("A1");
            bool result = vm.IsItemFound("A1");
            Assert.IsFalse(result);
        }//string slot

        [TestMethod]
        public void InStockTestAboveZero()
        {
            VendingMachine vm = new VendingMachine();
            vm.ReadFile();
            bool result = vm.InStock("A1");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsItemFoundTrueTest()
        {
            VendingMachine vm = new VendingMachine();
            vm.ReadFile();
            bool result = vm.IsItemFound("A1");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsItemFoundFalseTest()
        {
            VendingMachine vm = new VendingMachine();
            vm.ReadFile();
            bool result = vm.IsItemFound("E5");
            Assert.IsFalse(result);
        }


        [TestMethod]
        public void TypeBoughtTest()
        {
            VendingMachine vm = new VendingMachine();
            vm.ReadFile();
            string result = vm.TypeBought("A1");
            Assert.AreEqual("A", result);
        }

        [TestMethod]
        public void ResetBalanceTest()
        {
            VendingMachine vm = new VendingMachine();
            vm.UserBalance = 5.00M;
            Assert.AreEqual(5.00M, vm.UserBalance);
            bool result = vm.ResetBalance();
            Assert.IsTrue(result);
            Assert.AreEqual(0.00M, vm.UserBalance);
        }

        [TestMethod]
        public void MakeChangeTest()
        {
            VendingMachine vm = new VendingMachine();
            Dictionary<string, int> result = new Dictionary<string, int>();
            result.Add("Quarters", 2);
            result.Add("Dimes", 0);
            result.Add("Nickels", 0);
            result.Add("Pennies", 1);
            vm.UserBalance = 0.51M;
            vm.MakeChange();
            Dictionary<string, int> newResult = vm.MakeChange();
            CollectionAssert.AreEqual(newResult, result);
            
        }

        [TestMethod]
        public void AddLogEntryFeedMoneyTest()
        {
            VendingMachine vm = new VendingMachine();
            bool result = vm.AddLogEntry("FEED MONEY", 500.00M);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AddLogEntryGIVECHANGETest()
        {
            VendingMachine vm = new VendingMachine();
            bool result = vm.AddLogEntry("GIVE CHANGE", 500.00M);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AddLogEntryElseTest()
        {
            VendingMachine vm = new VendingMachine();
            bool result = vm.AddLogEntry("Else", 500.00M);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DepositMoneyTest()
        {
            VendingMachine vm = new VendingMachine();
            vm.UserBalance = 5.00M;
            bool result = vm.DepositMoney(5.00M);
            Assert.IsTrue(result);
            Assert.AreEqual(10.00M, vm.UserBalance);
        }

        //Unit Tests for Vending Machine Item

        [TestMethod]
        public void VendingMachineItemTestCreate()
        {
            VendingMachineItem vmi = new VendingMachineItem();
            Assert.IsNotNull(vmi);
        }

        [TestMethod]
        public void VendingMachineItemTestProperties()
        {
            VendingMachineItem vmi = new VendingMachineItem();
            vmi.SlotLocation = "A1";
            vmi.SnackName = "Chips";
            vmi.Price = 3.05M;
            vmi.Count = 5;

            Assert.AreEqual("A1", vmi.SlotLocation);
            Assert.AreEqual("Chips", vmi.SnackName);
            Assert.AreEqual(3.05M, vmi.Price);
            Assert.AreEqual(5, vmi.Count);

            Assert.AreEqual("A1 - Chips - 3.05  ,  Quantity: 5", vmi.ToString());
        }

        [TestMethod]
        public void VendingMachineItemTestSOLDOUT()
        {
            VendingMachineItem vmi = new VendingMachineItem();
            vmi.SlotLocation = "A1";
            vmi.SnackName = "Chips";
            vmi.Price = 3.05M;
            vmi.Count = 0;

            Assert.AreEqual("A1", vmi.SlotLocation);
            Assert.AreEqual("Chips", vmi.SnackName);
            Assert.AreEqual(3.05M, vmi.Price);
            Assert.AreEqual(0, vmi.Count);

            Assert.AreEqual("A1 - Chips - 3.05  ,  Quantity: SOLD OUT", vmi.ToString());
        }


    }
}
