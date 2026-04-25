using WorkOrderSystem.Data;
using WorkOrderSystem.Models;

namespace WorkOrderSystem.Services
{
    public class DepartmentService
    {
        private AppDbContext context = new AppDbContext();

        public List<Department> GetAllDepartments()
        {
            return context.Departments.ToList();
        }

        public Department? GetDepartmentById(int id)
        {
            return context.Departments.FirstOrDefault(d => d.Id == id);
        }
    }
}