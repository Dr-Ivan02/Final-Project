using System;
using System.Collections.Generic;
using System.Text;
using WorkOrderSystem.Models;

namespace WorkOrderSystem.Services
{
    // Handles operations related to work orders
    public class WorkOrderService
    {
        private List<WorkOrder> workOrders = new List<WorkOrder>();

        public void CreateWorkOrder(WorkOrder order)
        {
            workOrders.Add(order);
        }

        public List<WorkOrder> GetAllWorkOrders()
        {
            return workOrders;
        }
    }
}
