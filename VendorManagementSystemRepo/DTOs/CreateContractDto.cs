using System.ComponentModel.DataAnnotations;
using VendorManagementSystemRepo.Models;
namespace VendorManagementSystemRepo.DTOs
{
    public class CreateContractDto
    {
        [Required]
        public string ContractNumber { get; set; } = string.Empty;
        [Required]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        


    }
}