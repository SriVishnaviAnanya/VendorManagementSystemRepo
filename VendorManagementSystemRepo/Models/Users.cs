using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace VendorManagementSystemRepo.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public bool IsActive { get; set; }
        
    }

   

    
}
