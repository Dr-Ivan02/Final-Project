using WorkOrderSystem.Models;
using WorkOrderSystem.Services;

var workOrderService = new WorkOrderService();
var authService = new AuthService();
var departmentService = new DepartmentService();

User? currentUser = null;

while (currentUser == null)
{
    Console.WriteLine("\n--- LOGIN ---");

    Console.Write("Username: ");
    string username = Console.ReadLine() ?? string.Empty;

    Console.Write("Password: ");
    string password = Console.ReadLine() ?? string.Empty;

    currentUser = authService.Login(username, password);

    if (currentUser == null)
    {
        Console.WriteLine("Invalid username or password.");
    }
}

Role currentRole;

if (currentUser.Role == "Supervisor")
{
    currentRole = new GeneralSupervisor();
    Console.WriteLine("\nWelcome, General Supervisor.");
    Console.ReadKey();
    Console.Clear();
}
else
{
    var department = departmentService.GetDepartmentById(currentUser.DepartmentId ?? 0);

    if (department == null)
    {
        Console.WriteLine("This user is not assigned to a valid department.");
        return;
    }

    currentRole = new DepartmentRole
    {
        DepartmentId = department.Id
    };

    Console.WriteLine($"\nWelcome, {department.Name} Department.");
    Console.ReadKey();
    Console.Clear();
}

string option;

do
{
    Console.Clear();
    currentRole.ShowMenu();

    Console.Write("Select option: ");
    option = Console.ReadLine();

    if (currentRole is GeneralSupervisor)
    {
        switch (option)
        {
            case "1": CreateWorkOrder(workOrderService, departmentService); break;
            case "2": ViewAllWorkOrders(workOrderService, departmentService); break;
            case "3": UpdateStatus(workOrderService); break;
            case "4": AddComment(workOrderService); break;
            case "5": ViewComments(workOrderService); break;
            case "6": DeleteWorkOrder(workOrderService); break;
        }
    }
    else if (currentRole is DepartmentRole deptRole)
    {
        switch (option)
        {
            case "1": ViewDepartmentOrders(workOrderService, departmentService, deptRole.DepartmentId); break;
            case "2": UpdateStatus(workOrderService); break;
            case "3": AddComment(workOrderService); break;
            case "4": ViewComments(workOrderService); break;
        }
    }

} while (option != "0");


// Methods section

// Method to create work order
void CreateWorkOrder(WorkOrderService service, DepartmentService departmentService)
{
    string title;
    do
    {
        Console.Write("Title: ");
        title = Console.ReadLine() ?? string.Empty;
    } while (string.IsNullOrWhiteSpace(title));

    string description;
    do
    {
        Console.Write("Description: ");
        description = Console.ReadLine() ?? string.Empty;
    } while (string.IsNullOrWhiteSpace(description));

    var departments = departmentService.GetAllDepartments();

    Console.WriteLine("\nAvailable Departments:");

    foreach (var department in departments)
    {
        Console.WriteLine($"{department.Id}. {department.Name}");
    }

    int departmentId;

    Console.Write("Select Department ID: ");
    while (!int.TryParse(Console.ReadLine(), out departmentId) ||
           !departments.Any(d => d.Id == departmentId))
    {
        Console.Write("Invalid department. Try again: ");
    }

    var order = new WorkOrder
    {
        Title = title,
        Description = description,
        Status = "Open",
        CreatedDate = DateTime.Now,
        DepartmentId = departmentId
    };

    service.CreateWorkOrder(order);

    Console.WriteLine("Work order created successfully.");
    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}

// Method to view all work orders (supervisor)
void ViewAllWorkOrders(WorkOrderService service, DepartmentService departmentService)
{
    var orders = service.GetAllWorkOrders();

    if (orders.Count == 0)
    {
        Console.WriteLine("No work orders found.");
        return;
    }

    foreach (var order in orders)
    {
        var department = departmentService.GetDepartmentById(order.DepartmentId);

        Console.WriteLine($"     WORK ORDER #{order.Id}       ");
        Console.WriteLine($"Title        : {order.Title}");
        Console.WriteLine($"Description  : {order.Description}");
        Console.WriteLine($"Status       : {order.Status}");
        Console.WriteLine($"Department   : {department?.Name} (ID: {order.DepartmentId})");
        Console.WriteLine($"Created Date : {order.CreatedDate:dd/MM/yyyy hh:mm tt}");
        
    }
    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}

// Method to update work order status
void UpdateStatus(WorkOrderService service)
{
    int id;
    Console.Write("Order Id: ");

    while (!int.TryParse(Console.ReadLine(), out id))
    {
        Console.Write("Invalid input. Enter a valid number: ");
    }

    var order = service.GetWorkOrderById(id);

    if (order == null)
    {
        Console.WriteLine("This work order does not exist.");
        return;
    }

    Console.WriteLine($"\nSelected work order: {order.Title}");
    Console.WriteLine($"Current status: {order.Status}");

    Console.WriteLine("\nSelect new status:");
    Console.WriteLine("1. Open");
    Console.WriteLine("2. In Progress");
    Console.WriteLine("3. Resolved");

    Console.Write("Option: ");
    string option = Console.ReadLine() ?? string.Empty;

    string newStatus;

    switch (option)
    {
        case "1":
            newStatus = "Open";
            break;
        case "2":
            newStatus = "In Progress";
            break;
        case "3":
            newStatus = "Resolved";
            break;
        default:
            Console.WriteLine("Invalid status option.");
            return;
    }

    service.UpdateWorkOrderStatus(id, newStatus);
    Console.WriteLine("Status updated successfully.");
    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}

// Method to add comment to work order
void AddComment(WorkOrderService service)
{
    int id;
    Console.Write("Order Id: ");

    while (!int.TryParse(Console.ReadLine(), out id))
    {
        Console.Write("Invalid input. Enter a valid number: ");
    }

    var order = service.GetWorkOrderById(id);

    if (order == null)
    {
        Console.WriteLine("This work order does not exist.");
        return;
    }

    if (currentRole is DepartmentRole deptRole && order.DepartmentId != deptRole.DepartmentId)
    {
        Console.WriteLine("You do not have access to this work order.");
        return;
    }

    Console.WriteLine($"\nSelected work order: {order.Title}");

    string text;
    do
    {
        Console.Write("Comment: ");
        text = Console.ReadLine() ?? string.Empty;
    } while (string.IsNullOrWhiteSpace(text));

    service.AddCommentToWorkOrder(id, text);
    Console.WriteLine("Comment added successfully.");
    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}
// Method to view comments for a work order
void ViewComments(WorkOrderService service)
{
    int id;
    Console.Write("Order Id: ");

    while (!int.TryParse(Console.ReadLine(), out id))
    {
        Console.Write("Invalid input. Enter a valid number: ");
    }

    var order = service.GetWorkOrderById(id);

    if (order == null)
    {
        Console.WriteLine("This work order does not exist.");
        return;
    }

    if (currentRole is DepartmentRole deptRole && order.DepartmentId != deptRole.DepartmentId)
    {
        Console.WriteLine("You do not have access to this work order.");
        return;
    }

    Console.WriteLine($"\n       COMMENTS FOR ORDER #{order.Id}      ");
    Console.WriteLine($"Title : {order.Title}");
    Console.WriteLine("----------------------------------------");

    var comments = service.GetCommentsByWorkOrder(id);

    if (comments.Count == 0)
    {
        Console.WriteLine("No comments found for this work order.");
        return;
    }

    foreach (var c in comments)
    {
        Console.WriteLine($"{c.Text} - {c.Date}");
    }
    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}

// Method to delete work order (supervisor only)
void DeleteWorkOrder(WorkOrderService service)
{
    var orders = service.GetAllWorkOrders();

    if (orders.Count == 0)
    {
        Console.WriteLine("No work orders available.");
        return;
    }

    foreach (var o in orders)
    {
        Console.WriteLine($"ID: {o.Id} | Title: {o.Title} | Status: {o.Status} | Department: {o.DepartmentId}");
    }

    int id;
    Console.Write("Select Work Order ID to delete: ");

    while (!int.TryParse(Console.ReadLine(), out id) || !orders.Any(o => o.Id == id))
    {
        Console.Write("Invalid ID. Try again: ");
    }

    var selected = orders.First(o => o.Id == id);

    Console.WriteLine($"\nSelected work order:");
    Console.WriteLine($"Title: {selected.Title}");
    Console.WriteLine($"Description: {selected.Description}");
    Console.WriteLine($"Status: {selected.Status}");
    Console.WriteLine($"Department: {selected.DepartmentId}");

    Console.Write("\nAre you sure you want to delete this work order? (y/n): ");
    var confirmation = Console.ReadLine();

    if (confirmation?.ToLower() == "y")
    {
        service.DeleteWorkOrder(id);
        Console.WriteLine("Work order deleted successfully.");
    }
    else
    {
        Console.WriteLine("Delete operation cancelled.");
    }
    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}

// Method to view work orders for a specific department
void ViewDepartmentOrders(WorkOrderService service, DepartmentService departmentService, int departmentId)
{
    var department = departmentService.GetDepartmentById(departmentId);
    var orders = service.GetWorkOrdersByDepartment(departmentId);

    if (orders.Count == 0)
    {
        Console.WriteLine($"No work orders assigned to {department?.Name ?? "this department"}.");
        return;
    }

    foreach (var order in orders)
    {
        Console.WriteLine($"        WORK ORDER #{order.Id}       ");
        Console.WriteLine($"Title        : {order.Title}");
        Console.WriteLine($"Description  : {order.Description}");
        Console.WriteLine($"Status       : {order.Status}");
        Console.WriteLine($"Department   : {department?.Name} (ID: {order.DepartmentId})");
        Console.WriteLine($"Created Date : {order.CreatedDate:dd/MM/yyyy hh:mm tt}");
    }
    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}