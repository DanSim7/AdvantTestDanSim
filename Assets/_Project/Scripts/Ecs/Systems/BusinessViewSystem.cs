using _Project.Scripts.Data.Business;
using _Project.Scripts.Data.Localization;
using _Project.Scripts.Ecs.Components;
using Leopotam.EcsLite;
using TMPro;
using UnityEngine.UI;

namespace _Project.Scripts.Ecs.Systems
{
    public class BusinessViewSystem : IEcsRunSystem
    {
        private readonly AllBusinessData _allBusinessData;
        private readonly LocalizationData _localizationData;

        public BusinessViewSystem(AllBusinessData allBusinessData, LocalizationData localizationData)
        {
            _allBusinessData = allBusinessData;
            _localizationData = localizationData;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
        
            var filter = world.Filter<Business>().Inc<BusinessView>().End();
            var walletFilter = world.Filter<Wallet>().End();
        
            float balance = 0;
            foreach (int walletEntity in walletFilter)
            {
                balance = world.GetPool<Wallet>().Get(walletEntity).Balance;
            }

            foreach (int entity in filter)
            {
                ref var business = ref world.GetPool<Business>().Get(entity);
                ref var view = ref world.GetPool<BusinessView>().Get(entity);

                view.ProgressBar.fillAmount = business.Progress;
            
                view.LevelText.text = $"LVL\n{business.Level}";
                
                var businessData = _allBusinessData.BusinessDatas[business.ID];
            
                var income = Utilities.Utils.CalculateIncome(business, businessData);
                view.IncomeText.text = $"Доход\n{income:F2}$";
            
                var levelUpPrice = (business.Level + 1) * businessData.BaseCost;
                view.LevelUpPriceText.text = $"LVL UP\nЦена: {levelUpPrice:F0}$";
            
                view.LevelUpButton.interactable = balance >= levelUpPrice;

                var localizationBusiness = _localizationData.Businesses[business.ID];
            
                UpdateUpgradeButton(ref view.Upgrade1Button, ref view.Upgrade1Text, 
                    business.Upgrade1Purchased, 
                    businessData.Upgrade1,
                    localizationBusiness.Upgrade1Name);
            
                UpdateUpgradeButton(ref view.Upgrade2Button, ref view.Upgrade2Text, 
                    business.Upgrade2Purchased, 
                    businessData.Upgrade2,
                    localizationBusiness.Upgrade2Name);
            }
        }

        private void UpdateUpgradeButton(ref Button button, ref TextMeshProUGUI text, bool purchased, in UpgradeData upgradeData, string name)
        {
            if (purchased)
            {
                button.interactable = false;
                text.text = $"{name}\nДоход: +{upgradeData.IncomeMultiplier * 100:F0}%\nКуплено";
            }
            else
            {
                button.interactable = true;
                text.text = $"{name}\nДоход: +{upgradeData.IncomeMultiplier * 100:F0}%\nЦена: {upgradeData.Cost}$";
            }
        }
    }
}