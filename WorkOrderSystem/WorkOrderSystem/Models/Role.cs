
namespace WorkOrderSystem.Models
{
    // Abstract base class for user roles
    public abstract class Role
    {
        public string Username { get; set; } = string.Empty;

        public abstract void ShowMenu();
    }
}

