using _Project.Scripts.Data.Business;
using _Project.Scripts.Data.Localization;
using _Project.Scripts.Ecs.Components;
using _Project.Scripts.Ecs.Views;
using Leopotam.EcsLite;
using BusinessView = _Project.Scripts.Ecs.Components.BusinessView;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Ecs.Systems
{
    public class InitBusinessSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world;
        private readonly AllBusinessData _allBusinessData;
        private readonly LocalizationData _localizationData;
        private readonly MainView _mainView;
        private readonly SaveData _saveData;

        public InitBusinessSystem(EcsWorld world, AllBusinessData allBusinessData, LocalizationData localizationData,
            MainView mainView, SaveData saveData)
        {
            _world = world;
            _allBusinessData = allBusinessData;
            _localizationData = localizationData;
            _mainView = mainView;
            _saveData = saveData;
        }

        public void Init(IEcsSystems systems)
        {
            var walletEntity = _world.NewEntity();
            ref var wallet = ref _world.GetPool<Wallet>().Add(walletEntity);
            wallet.Balance = _saveData?.Balance ?? 0;

            _mainView.BalanceText.text = $"${wallet.Balance:F2}";

            for (int i = 0; i < _allBusinessData.BusinessDatas.Count; i++)
                CreateBusinessEntity(i);
        }

        private void CreateBusinessEntity(int businessId)
        {
            var entity = _world.NewEntity();
            ref var business = ref _world.GetPool<Business>().Add(entity);
            business.ID = businessId;

            var savedBusiness = _saveData?.Businesses?.Find(b => b.ID == businessId);
        
            business.Level = savedBusiness?.Level ?? (businessId == 0 ? 1 : 0);
            business.IsPurchased = savedBusiness?.Level > 0 || businessId == 0;
            business.Progress = savedBusiness?.Progress ?? 0f;
            business.Upgrade1Purchased = savedBusiness?.Upgrade1Purchased ?? false;
            business.Upgrade2Purchased = savedBusiness?.Upgrade2Purchased ?? false;

            InitBusinessView(entity, businessId);
        }

        private void InitBusinessView(int entity, int businessId)
        {
            var businessView = Object.Instantiate(
                _mainView.BusinessViewPrefab,
                _mainView.BusinessesContainer
            );

            var view = new BusinessView
            {
                TitleText = businessView.TitleText,
                LevelText = businessView.LevelText,
                IncomeText = businessView.IncomeText,
                ProgressBar = businessView.ProgressBar,
                LevelUpButton = businessView.LevelUpButton,
                LevelUpPriceText = businessView.LevelUpPriceText,
                Upgrade1Button = businessView.Upgrade1Button,
                Upgrade1Text = businessView.Upgrade1Text,
                Upgrade2Button = businessView.Upgrade2Button,
                Upgrade2Text = businessView.Upgrade2Text
            };

            var localizationBusiness = _localizationData.Businesses[businessId];
            view.TitleText.text = localizationBusiness.Name;
            view.Upgrade1Text.text = localizationBusiness.Upgrade1Name;
            view.Upgrade2Text.text = localizationBusiness.Upgrade2Name;

            ref var viewComp = ref _world.GetPool<BusinessView>().Add(entity);
            viewComp = view;

            view.LevelUpButton.onClick.AddListener(() => OnLevelUpButton(businessId));
            view.Upgrade1Button.onClick.AddListener(() => OnUpgradeButton(businessId, 1));
            view.Upgrade2Button.onClick.AddListener(() => OnUpgradeButton(businessId, 2));
        }

        private void OnLevelUpButton(int businessId)
        {
            var eventEntity = _world.NewEntity();
            ref var evt = ref _world.GetPool<LevelUpEvent>().Add(eventEntity);
            evt.BusinessID = businessId;
        }

        private void OnUpgradeButton(int businessId, int upgradeId)
        {
            var eventEntity = _world.NewEntity();
            ref var evt = ref _world.GetPool<UpgradeEvent>().Add(eventEntity);
            evt.BusinessID = businessId;
            evt.UpgradeID = upgradeId;
        }
    }
}