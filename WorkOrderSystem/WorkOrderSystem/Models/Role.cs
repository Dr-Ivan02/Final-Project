using System;
using System.Collections.Generic;
using System.Text;

namespace WorkOrderSystem.Models
{
    // This class is used for different system roles (e.g., Admin, User) to determine access levels and permissions
    public abstract class Role
    {
        public string RoleName { get; set; }

        public Role(string roleName)
        {
            RoleName = roleName;
        }
        public abstract void ShowMenu();
    }
}
