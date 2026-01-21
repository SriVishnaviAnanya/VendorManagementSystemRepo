using GovernanceApi.Models;

namespace GovernanceApi.Services
{
    public class PerformanceService
    {
        public int CalculatePenalty(List<NonComplianceLog> issues)
        {
            int penalty = 0;

          foreach (var issue in issues)
            {
                if (string.IsNullOrWhiteSpace(issue.Severity))
              
                {
                    penalty += 5;
                    continue;
                }
                var severity = issue.Severity.Trim().ToLower();
                if(severity== "major")
                
                {
                    penalty += 20;
                }
                else { penalty += 5; }
            }

            return penalty;
        }

        public VendorPerformance CalculateFinalPerformance(
            VendorPerformance p,
            int penalty,decimal calculatedValue)
        {
            decimal issueScore = Math.Max(0, 100 - penalty);
            p.FinalScore = (int)Math.Round(calculatedValue, 0);
            decimal score = (p.ComplianceScore * 0.40m) + (p.DeliveryQuality * 0.25m) + (p.SLAAdherence * 0.20m) + (issueScore * 0.15m);
            p.FinalScore= Math.Round(score, 2);
            p.VendorRating = GetVendorRating(p.FinalScore).ToString(); 
            return p;
        }

        private string GetVendorRating(decimal score)
        {
            if (score >= 85) return "Excellent";
            if (score >= 70) return "Good";
            if (score >= 50) return "Fair";
            return "Poor";
        }
    }
}