using System;
using System.Collections.Generic;
using System.Text;

namespace WorkOrderSystem.Models
{
    // Represents a department-level role that interacts with assigned work orders
    public class DepartmentRole : Role
    {
        public int DepartmentId { get; set; }

        public DepartmentRole(int departmentId, string roleName) : base(roleName)
        {
            DepartmentId = departmentId;
        }

        public override void ShowMenu()
        {
            Console.WriteLine("1. View Assigned Work Orders");
            Console.WriteLine("2. Update Work Order Status");
            Console.WriteLine("3. Add Comment to Work Order");
        }
    }
}
