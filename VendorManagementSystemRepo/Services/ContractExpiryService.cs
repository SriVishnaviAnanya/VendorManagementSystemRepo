using VendorManagementSystemRepo.Data;

using VendorManagementSystemRepo.Models;

using Microsoft.Extensions.Hosting;

using Microsoft.EntityFrameworkCore;

public class ContractExpiryService : BackgroundService

{

    private readonly IServiceScopeFactory _scopeFactory;

    public ContractExpiryService(IServiceScopeFactory scopeFactory)

    {

        _scopeFactory = scopeFactory;

    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)

    {

        while (!stoppingToken.IsCancellationRequested)

        {

            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<VendorManagementSystemDb>();

            var expiredContracts = await context.Contracts

                .Where(c => c.EndDate < DateTime.UtcNow &&

                            c.Status == ContractStatus.Active)

                .ToListAsync();

            foreach (var contract in expiredContracts)

            {

                contract.Status = ContractStatus.Expired;

            }

            await context.SaveChangesAsync();

            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);

        }

    }

}
