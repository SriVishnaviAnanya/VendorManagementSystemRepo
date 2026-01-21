using VendorManagementSystemRepo.DTOs;
using VendorManagementSystemRepo.Models;

namespace VendorManagementSystemRepo.Services
{
    public interface IComplianceService
    {
        ComplianceResultDto EvaluateCompliance(ComplianceChecklist checklist);
    }
}
