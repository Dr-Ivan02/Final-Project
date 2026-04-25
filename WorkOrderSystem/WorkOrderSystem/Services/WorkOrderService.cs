using WorkOrderSystem.Models;
using WorkOrderSystem.Data;


namespace WorkOrderSystem.Services
{
    // Handles operations related to work orders
    public class WorkOrderService
    {
        private AppDbContext context = new AppDbContext();

        public void CreateWorkOrder(WorkOrder order)
        {
            context.WorkOrders.Add(order);
            context.SaveChanges();
        }

        public List<WorkOrder> GetAllWorkOrders()
        {
            return context.WorkOrders.ToList();
        }

        // Filters work orders by department
        public List<WorkOrder> GetWorkOrdersByDepartment(int departmentId)
        {
            return context.WorkOrders
                .Where(o => o.DepartmentId == departmentId)
                .ToList();
        }

        // Updates the current state of a work order (Open, In Progress, Resolved)
        public void UpdateWorkOrderStatus(int orderId, string newStatus)
        {
            var order = context.WorkOrders.FirstOrDefault(o => o.Id == orderId);

            if (order != null)
            {
                order.Status = newStatus;
                context.SaveChanges();
            }
        }

        // Adds a comment to a specific work order for tracking updates
        public void AddCommentToWorkOrder(int workOrderId, string text)
        {
            var comment = new Comment(workOrderId, text);
            context.Comments.Add(comment);
            context.SaveChanges();
        }

        // Retrieves all comments associated with a specific work order for better communication and tracking
        public List<Comment> GetCommentsByWorkOrder(int workOrderId)
        {
            return context.Comments
                .Where(c => c.WorkOrderId == workOrderId)
                .ToList();
        }

        // Deletes a work order from the system
        public void DeleteWorkOrder(int id)
        {
            var order = context.WorkOrders.FirstOrDefault(o => o.Id == id);

            if (order != null)
            {
                var comments = context.Comments
                    .Where(c => c.WorkOrderId == id)
                    .ToList();

                context.Comments.RemoveRange(comments);
                context.WorkOrders.Remove(order);
                context.SaveChanges();
            }
        }

        // Retrieves a specific work order by its unique identifier
        public WorkOrder? GetWorkOrderById(int id)
        {
            return context.WorkOrders.FirstOrDefault(o => o.Id == id);
        }

        // Deletes all comments associated with a specific work order
        public void DeleteCommentsByWorkOrderId(int workOrderId)
        {
            var comments = context.Comments
                .Where(c => c.WorkOrderId == workOrderId)
                .ToList();

            context.Comments.RemoveRange(comments);
            context.SaveChanges();
        }
    }
}