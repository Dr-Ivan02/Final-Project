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
        private List<Comment> comments = new List<Comment>();

        public void CreateWorkOrder(WorkOrder order)
        {
            workOrders.Add(order);
        }

        public List<WorkOrder> GetAllWorkOrders()
        {
            return workOrders;
        }

        // Filters work orders by department for better organization and management

        public List<WorkOrder> GetWorkOrdersByDepartment(int departmentId)
        {
            List<WorkOrder> result = new List<WorkOrder>();

            foreach (var order in workOrders)
            {
                if (order.DepartmentId == departmentId)
                {
                    result.Add(order);
                }
            }

            return result;
        }

        // Updates the current state of a work order (Open, In Progress, Resolved)

        public void UpdateWorkOrderStatus(int orderId, string newStatus)
        {
            foreach (var order in workOrders)
            {
                if (order.Id == orderId)
                {
                    order.Status = newStatus;
                    break;
                }
            }
        }

        // Adds a comment to a specific work order for tracking updates
        public void AddCommentToWorkOrder(int workOrderId, string text)
        {
            Comment comment = new Comment(workOrderId, text);
            comments.Add(comment);
        }

        // Retrieves all comments associated with a specific work order for better communication and tracking
        public List<Comment> GetCommentsByWorkOrder(int workOrderId)
        {
            List<Comment> result = new List<Comment>();

            foreach (var comment in comments)
            {
                if (comment.WorkOrderId == workOrderId)
                {
                    result.Add(comment);
                }
            }

            return result;
        }


    }
}