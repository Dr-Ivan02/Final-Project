using WorkOrderSystem.Models;
using WorkOrderSystem.Services;

var service = new WorkOrderService();

string option;

do
{
    Console.WriteLine("\n--- WORK ORDER SYSTEM ---");
    Console.WriteLine("1. Create Work Order");
    Console.WriteLine("2. View All Work Orders");
    Console.WriteLine("3. Update Work Order Status");
    Console.WriteLine("4. Add Comment");
    Console.WriteLine("5. View Comments");
    Console.WriteLine("0. Exit");

    Console.Write("Select an option: ");
    option = Console.ReadLine();

    switch (option)
    {
        case "1":
            CreateWorkOrder(service);
            break;

        case "2":
            ViewAllWorkOrders(service);
            break;

        case "3":
            UpdateStatus(service);
            break;

        case "4":
            AddComment(service);
            break;

        case "5":
            ViewComments(service);
            break;

        case "0":
            Console.WriteLine("Exiting...");
            break;

        default:
            Console.WriteLine("Invalid option.");
            break;
    }

} while (option != "0");



// Methods section

// Method to create work order
void CreateWorkOrder(WorkOrderService service)
{
    Console.Write("Title: ");
    var title = Console.ReadLine();

    Console.Write("Description: ");
    var description = Console.ReadLine();

    Console.Write("Department Id: ");
    int departmentId = int.Parse(Console.ReadLine());

    var order = new WorkOrder
    {
        Title = title,
        Description = description,
        Status = "Open",
        CreatedDate = DateTime.Now,
        DepartmentId = departmentId
    };

    service.CreateWorkOrder(order);
    Console.WriteLine("Work order created.");
}

// Method to view all work orders
void ViewAllWorkOrders(WorkOrderService service)
{
    var orders = service.GetAllWorkOrders();

    foreach (var o in orders)
    {
        Console.WriteLine($"{o.Id} | {o.Title} | {o.Status} | Dept: {o.DepartmentId}");
    }
}

// Method to update work order status
void UpdateStatus(WorkOrderService service)
{
    Console.Write("Order Id: ");
    int id = int.Parse(Console.ReadLine());

    Console.Write("New Status: ");
    var status = Console.ReadLine();

    service.UpdateWorkOrderStatus(id, status);
    Console.WriteLine("Status updated.");
}

// Method to add comment to work order
void AddComment(WorkOrderService service)
{
    Console.Write("Order Id: ");
    int id = int.Parse(Console.ReadLine());

    Console.Write("Comment: ");
    var text = Console.ReadLine();

    service.AddCommentToWorkOrder(id, text);
    Console.WriteLine("Comment added.");
}

// Method to view comments for a work order
void ViewComments(WorkOrderService service)
{
    Console.Write("Order Id: ");
    int id = int.Parse(Console.ReadLine());

    var comments = service.GetCommentsByWorkOrder(id);

    foreach (var c in comments)
    {
        Console.WriteLine($"{c.Text} - {c.Date}");
    }
}

