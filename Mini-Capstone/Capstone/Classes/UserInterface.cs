using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class UserInterface
    {
        private VendingMachine vendingMachine = new VendingMachine();
        //several methods in this class will be created.  the pet example is a great example of this 
        public void RunInterface()
        {
            bool done = false;
            vendingMachine.ReadFile();
            while (!done)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("(1) Display Vending Machine Items");
                Console.WriteLine("(2) Purchase");
                Console.WriteLine("(3) End");
                Console.WriteLine();

                int choice = 0;
                try
                {
                    choice = Int32.Parse(Console.ReadLine());
                }
                catch
                {
                    choice = 0;
                }

                switch (choice)
                {
                    case 1:
                        DisplayVendingMachineItems();
                        break;
                    case 2:
                        PurchaseItems();
                        break;
                    case 3:
                        done = true;
                        break;
                    default:
                        Console.WriteLine("Invalid Choice.  Please try again.");
                        break;
                }
            }

        }

        private void DisplayVendingMachineItems()
        {
            VendingMachineItem[] result = vendingMachine.ToArray();
            foreach (VendingMachineItem item in result)
            {
                Console.WriteLine(item.ToString());
            }
        }

        private void PurchaseItems()
        {
            bool done = false;
            while (!done)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("(1) Feed Money");
                Console.WriteLine("(2) Select Product");
                Console.WriteLine("(3) Finish Transaction");
                Console.WriteLine($"Curent Money Provided: ${vendingMachine.UserBalance}");
                Console.WriteLine();

                int choice = 0;
                try
                {
                    choice = Int32.Parse(Console.ReadLine());
                }
                catch
                {
                    choice = 0;
                }

                switch (choice)
                {
                    case 1:
                        AddMoney();
                        break;
                    case 2:
                        SelectProduct();
                        break;
                    case 3:
                        //TODO add methods to calc and produce change 
                        FinishTransaction();
                        vendingMachine.ResetBalance();
                        done = true;
                        break;
                    default:
                        Console.WriteLine("Invalid Choice.  Please try again.");
                        break;
                }
            }
        }

        public void AddMoney()
        {
            Console.WriteLine("Please enter the amount to add (ex. 5.00): ");
            try
            {
                if (vendingMachine.DepositMoney(decimal.Parse(Console.ReadLine())))
                {
                    Console.WriteLine("Deposit Successful!");
                }
                else
                {
                    Console.WriteLine("Invalid Deposit");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Entry");
                Console.WriteLine(ex.Message);
            }
        }

        public void SelectProduct()
        {
            DisplayVendingMachineItems();
            Console.WriteLine("Please enter the product (ex. Slot Number): ");
            string slot = Console.ReadLine();
            slot = slot.ToUpper();
            if (vendingMachine.IsItemFound(slot))
            {
                if (vendingMachine.InStock(slot))
                {
                    if (vendingMachine.FundsSufficient(slot))
                    {
                        vendingMachine.SelectProductForPurchase(slot);
                        string type = vendingMachine.TypeBought(slot);
                        if (type == "A")
                        {
                            Console.WriteLine("Crunch Crunch, Yum!");
                        }
                        else if (type == "B")
                        {
                            Console.WriteLine("Munch Munch, Yum!");
                        }
                        else if (type == "C")
                        {
                            Console.WriteLine("Glug Glug, Yum!");
                        }
                        else if (type == "D")
                        {
                            Console.WriteLine("Chew Chew, Yum!");
                        }
                        else
                        {
                            Console.WriteLine("There was an error.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Insufficient Funds!");
                    }
                }
                else
                {
                    Console.WriteLine("Item is Sold Out!");
                }
            }
            else
            {
                Console.WriteLine("Item not found!");
            }

        }

        public void FinishTransaction()
        {
            Dictionary<string, int> result = vendingMachine.MakeChange();
            Console.WriteLine("Your change is: ");
            foreach (KeyValuePair<string, int> amount in result)
            {
                Console.WriteLine(amount.Value + " " + amount.Key);
            }
        }

    }
}
