using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Capstone.Classes
{
    public class VendingMachine
    {
        //must remain private, only methods in the class will access.  will copy to array if we need to send back 
        private List<VendingMachineItem> items = new List<VendingMachineItem>();
        private string filePath = @"C:\VendingMachine";
        private string fileName = "vendingmachine.csv";
        public decimal UserBalance { get; set; }
        public string SlotNumber { get; set; }
        //should be fully testable, no read or write lines 
        //userinterface will not be testable 

        //contrctor to read file and intialize vending machine 
        public bool ReadFile()
        {
            bool result = false;
            string path = Path.Combine(filePath, fileName);
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    items.Clear();
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] snackInfo = line.Split('|');
                        VendingMachineItem vendingMachineItem = new VendingMachineItem();
                        vendingMachineItem.SlotLocation = snackInfo[0];
                        vendingMachineItem.SnackName = snackInfo[1];
                        try
                        {
                            vendingMachineItem.Price = decimal.Parse(snackInfo[2]);
                        }
                        catch
                        {
                            //TODO ask john if we will be passed non decimal price
                            vendingMachineItem.Price = 0.00M;
                        }
                        vendingMachineItem.Count = 5;
                        items.Add(vendingMachineItem);
                    }
                }
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;

        }

        public VendingMachineItem[] ToArray()
        {
            VendingMachineItem[] result = items.ToArray();
            return result;
        }

        public bool SelectProductForPurchase(string slot)
        {
            foreach (VendingMachineItem test in items)
            {
                if (test.SlotLocation == slot)
                {
                    test.Count--;
                    AddLogEntry(test.SnackName, test.Price);
                    UserBalance -= test.Price;
                    return true;

                }
            }
            return false;
        }

        public bool FundsSufficient(string slot)
        {
            foreach (VendingMachineItem item in items)
            {
                if (item.SlotLocation == slot)
                {
                    if (item.Price < UserBalance)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool InStock(string slot)
        {
            foreach (VendingMachineItem item in items)
            {
                if (item.SlotLocation == slot)
                {
                    if (item.Count > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool IsItemFound(string slot)
        {
            foreach (VendingMachineItem item in items)
            {
                if (item.SlotLocation == slot)
                {
                    return true;
                }
            }
            return false;
        }

        public string TypeBought(string slot)
        {
            foreach (VendingMachineItem item in items)
            {
                if (item.SlotLocation == slot)
                {
                    string type = item.SlotLocation.Substring(0, 1);
                    return type;
                }
            }
            return "";
        }

        public bool ResetBalance()
        {
            UserBalance = 0.00M;
            return true;
        }

        public Dictionary<string, int> MakeChange()
        {
            Dictionary<string, int> changeDictionary = new Dictionary<string, int>();

            decimal quarter = 0.25M;
            decimal dime = 0.10M;
            decimal nickel = 0.05M;
            decimal penny = 0.01M;

            changeDictionary["Quarters"] = (int)(UserBalance / quarter);
            changeDictionary["Dimes"] = (int)((UserBalance % quarter) / dime);
            changeDictionary["Nickels"] = (int)(((UserBalance % quarter) % dime) / nickel);
            changeDictionary["Pennies"] = (int)((((UserBalance % quarter) % dime) % nickel) / penny);

            AddLogEntry("GIVE CHANGE", 0.00M);
            return changeDictionary;

        }

        public bool AddLogEntry(string action, decimal cost)
        {
            bool result = false;
            string logPath = "Log.txt";
            string path = Path.Combine(filePath, logPath); //TODO maybe change this 
            try
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    if (action == "FEED MONEY")
                    {
                        string line = $"{DateTime.Now} {action} {cost} {UserBalance}";
                        sw.WriteLine(line);
                        result = true;
                    }
                    else if (action == "GIVE CHANGE")
                    {
                        string line = $"{DateTime.Now} {action} {UserBalance} {cost}";
                        sw.WriteLine(line);
                        result = true;
                    }
                    else
                    {
                        string line = $"{DateTime.Now} {action} {UserBalance} {UserBalance - cost}";
                        sw.WriteLine(line);
                        result = true;
                    }
                }
            }

            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public bool DepositMoney(decimal funds)
        {
            UserBalance += funds;
            AddLogEntry("FEED MONEY", funds);
            return true;
        }
    }
}



