namespace WorkOrderSystem.Models
{
    // Represents a department within the organization
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Department() { }
        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

}


