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
            // Method to create order
            break;

        case "2":
            // Method to view order
            break;

        case "3":
            // Method to update order status
            break;

        case "4":
            // Method to add comments
            break;

        case "5":
            // Method to view comments
            break;

        case "0":
            Console.WriteLine("Exiting...");
            break;

        default:
            Console.WriteLine("Invalid option.");
            break;
    }

} while (option != "0");