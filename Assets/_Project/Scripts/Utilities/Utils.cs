using _Project.Scripts.Data.Business;
using _Project.Scripts.Ecs.Components;

namespace _Project.Scripts.Utilities
{
    public static class Utils
    {
        public static float CalculateIncome(Business business, BusinessData data)
        {
            var multiplier = 1f;
            
            if (business.Upgrade1Purchased)
                multiplier += data.Upgrade1.IncomeMultiplier;
            if (business.Upgrade2Purchased)
                multiplier += data.Upgrade2.IncomeMultiplier;
            
            return business.Level * data.BaseIncome * multiplier;
        }
    }
}