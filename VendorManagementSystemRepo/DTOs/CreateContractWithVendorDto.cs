using System.ComponentModel.DataAnnotations;
namespace Dev3_Contract.DTOs
{
    public class CreateContractWithVendorDto
    {
        [Required]
        public int VendorId { get; set; }
        [Required]
        public string ContractNumber { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string KeyTermsSummary { get; set; }
        public ContractType ContractType { get; set; }
        public decimal ContractValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class ContractType
    {
        public static ContractType MSA { get; internal set; }
    }
}
