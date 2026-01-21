using GovernanceApi.DTOs;
using GovernanceApi.Models;

namespace GovernanceApi.Services
{
    public interface IComplianceService
    {
        ComplianceResultDto EvaluateCompliance(ComplianceChecklist checklist);
    }
}
