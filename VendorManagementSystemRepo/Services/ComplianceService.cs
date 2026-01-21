//using VendorManagementSystemRepo.Models;

//namespace VendorManagementSystemRepo.Services
//{
//    public class ComplianceService:IComplianceService
//    {
//        public void EvaluateCompliance(ComplianceChecklist c)
//        {
//            int score = 0;
//            if(c.NDASigned)
//                score += 40;
//            if(c.CertificationsValid)
//                score += 30;
//            if(c.RegulatoryCompliant)
//                score += 30;
//            c.ComplianceScore = score;
//            if(score >= 85)
//                c.ComplianceStatus = "Compliant";
//            else if (score >= 60)
//                c.ComplianceStatus = "Partially Compliant";
//            else
//                c.ComplianceStatus = "Non-Compliant";
//            c.LastReviewDate = DateTime.Now;
//        }

//}



using VendorManagementSystemRepo.DTOs;
using VendorManagementSystemRepo.Models;

namespace VendorManagementSystemRepo.Services
{
    public class ComplianceService : IComplianceService
    {
        public ComplianceResultDto EvaluateCompliance(ComplianceChecklist checklist)
        {
            int passed = 0;

            if (checklist.NDASigned) passed++;
            if (checklist.CertificationsValid) passed++;
            if (checklist.RegulatoryCompliant) passed++;

            int score = (int)Math.Round((passed / 3.0) * 100);

            string status =
                score == 100 ? "Compliant" :
                score >= 50 ? "Partially Compliant" :
                "Non-Compliant";

            return new ComplianceResultDto
            {
                ComplianceScore = score,
                ComplianceStatus = status
            };
        }
    }
}