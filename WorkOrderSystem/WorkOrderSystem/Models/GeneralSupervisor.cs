
namespace WorkOrderSystem.Models
{
    // Represents the general supervisor who manages all work orders
    public class GeneralSupervisor : Role
    {
        public override void ShowMenu()
        {
            Console.WriteLine("\n--- SUPERVISOR MENU ---");
            Console.WriteLine("1. Create Work Order");
            Console.WriteLine("2. View All Work Orders");
            Console.WriteLine("3. Update Work Order Status");
            Console.WriteLine("4. Add Comment");
            Console.WriteLine("5. View Comments");
            Console.WriteLine("6. Delete Work Order");
            Console.WriteLine("0. Exit");
        }
    }
}
