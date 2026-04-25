namespace WorkOrderSystem.Models
{
    // Represents a work order assigned within the system
    public class WorkOrder
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int DepartmentId { get; set; }

        public WorkOrder() { }
        public WorkOrder(string title, string description, int departmentId)
        {
            Title = title;
            Description = description;
            Status = "Open";
            CreatedDate = DateTime.Now;
            DepartmentId = departmentId;
        }
    }

}
