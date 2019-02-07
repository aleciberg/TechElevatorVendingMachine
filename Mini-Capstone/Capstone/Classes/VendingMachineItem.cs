using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class VendingMachineItem
    {
        public string SlotLocation { get; set; }
        public string SnackName { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }

        public override string ToString()
        {
            if (Count > 0)
            {
                return $"{SlotLocation} - {SnackName} - {Price}  ,  Quantity: {Count}";
            }
            else
            {
                return $"{SlotLocation} - {SnackName} - {Price}  ,  Quantity: SOLD OUT";
            }
        }
    }
}
