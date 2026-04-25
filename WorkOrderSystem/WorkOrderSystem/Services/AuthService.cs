using WorkOrderSystem.Data;
using WorkOrderSystem.Models;

namespace WorkOrderSystem.Services
{
    public class AuthService
    {
        private AppDbContext context = new AppDbContext();

        public User? Login(string username, string password)
        {
            return context.Users
                .FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}