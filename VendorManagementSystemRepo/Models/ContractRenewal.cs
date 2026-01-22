public class ContractRenewal

{

    public int Id { get; set; }

    public Guid ContractId { get; set; }

    public Guid VendorId { get; set; }

    public string ContractNumber { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string RenewalStatus { get; set; } // Pending, Renewed, Expired

    public DateTime AlertSentDate { get; set; }

}
