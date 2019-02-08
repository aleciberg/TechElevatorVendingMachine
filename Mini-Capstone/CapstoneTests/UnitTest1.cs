using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;
using System.Collections.Generic;

namespace CapstoneTests
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TypeBoughtTest()
        {
            VendingMachine vm = new VendingMachine();
            vm.ReadFile();
            //List<VendingMachineItem> items = new List<VendingMachineItem>();
            //VendingMachineItem vendingMachineItem = new VendingMachineItem();
            //vendingMachineItem.SlotLocation = "A1";
            //vendingMachineItem.SnackName = "Potato Crisps";
            //vendingMachineItem.Price = 1.65M;
            //items.Add(vendingMachineItem);
            string test = vm.TypeBought("A1");
            Assert.AreEqual("A", test);
        }
    }
}
