public class RenewalService

{

    private readonly VclmDbContext _context;

    public RenewalService(VclmDbContext context)

    {

        _context = context;

    }

    public async Task CheckExpiries()

    {

        var upcoming = _context.ContractRenewals

            .Where(x => x.EndDate <= DateTime.UtcNow.AddDays(60)
&& x.RenewalStatus == "Pending")

            .ToList();

        foreach (var c in upcoming)

        {

            c.AlertSentDate = DateTime.UtcNow;

        }

        await _context.SaveChangesAsync();

    }

}
