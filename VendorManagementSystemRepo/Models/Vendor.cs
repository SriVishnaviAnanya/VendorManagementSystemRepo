using System;
using System.ComponentModel.DataAnnotations;

public class Vendor
{
    public int VendorId { get; set; }

    [Required]
    public string VendorName { get; set; }

    [Required]
    public string Category { get; set; }

    public string Email { get; set; }
    public string Phone { get; set; }

    public string Status { get; set; } = "Active"; // Active / Suspended

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}