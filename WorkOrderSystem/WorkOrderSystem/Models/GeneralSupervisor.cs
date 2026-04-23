using System;
using System.Collections.Generic;
using System.Text;

namespace WorkOrderSystem.Models
{
    // Represents the general supervisor who manages all work orders
    public class GeneralSupervisor : Role
    {
        public GeneralSupervisor() : base("General Supervisor")
        {
        }

        public override void ShowMenu()
        {
            Console.WriteLine("1. Create Work Order");
            Console.WriteLine("2. View All Work Orders");
        }
    }
}
