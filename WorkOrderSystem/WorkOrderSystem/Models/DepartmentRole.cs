namespace WorkOrderSystem.Models
{
    // Represents a department user with access limited
    public class DepartmentRole : Role
    {
        public int DepartmentId { get; set; }

        public override void ShowMenu()
        {
            Console.WriteLine("\n--- DEPARTMENT MENU ---");
            Console.WriteLine("1. View My Work Orders");
            Console.WriteLine("2. Update Work Order Status");
            Console.WriteLine("3. Add Comment");
            Console.WriteLine("4. View Comments");
            Console.WriteLine("0. Exit");
        }
    }
}