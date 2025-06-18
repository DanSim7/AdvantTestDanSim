using _Project.Scripts.Data.Business;
using _Project.Scripts.Ecs.Components;
using Leopotam.EcsLite;

namespace _Project.Scripts.Ecs.Systems
{
    public class UpgradeSystem : IEcsRunSystem
    {
        private readonly AllBusinessData _allBusinessData;

        public UpgradeSystem(AllBusinessData allBusinessData)
        {
            _allBusinessData = allBusinessData;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var events = world.Filter<UpgradeEvent>().End();
            var walletFilter = world.Filter<Wallet>().End();
        
            if (walletFilter.GetEntitiesCount() == 0)
                return;
        
            var walletEntity = walletFilter.GetRawEntities()[0];
            ref var wallet = ref world.GetPool<Wallet>().Get(walletEntity);
            var eventPool = world.GetPool<UpgradeEvent>();
            var businessPool = world.GetPool<Business>();
        
            foreach (var eventEntity in events)
            {
                ref var evt = ref eventPool.Get(eventEntity);
            
                int businessEntity = -1;
                var businessFilter = world.Filter<Business>().End();
                foreach (var entity in businessFilter)
                {
                    ref var businessComponent = ref businessPool.Get(entity);
                    if (businessComponent.ID == evt.BusinessID)
                    {
                        businessEntity = entity;
                        break;
                    }
                }
            
                if (businessEntity == -1) 
                    continue;
            
                ref var business = ref businessPool.Get(businessEntity);
                var data = _allBusinessData.BusinessDatas[business.ID];
            
                var upgradeCost = evt.UpgradeID == 1 ? 
                    data.Upgrade1.Cost : 
                    data.Upgrade2.Cost;
            
                var isPurchased = evt.UpgradeID == 1 ? 
                    business.Upgrade1Purchased : 
                    business.Upgrade2Purchased;
            
                if (!isPurchased && wallet.Balance >= upgradeCost)
                {
                    wallet.Balance -= upgradeCost;
                
                    if (evt.UpgradeID == 1)
                        business.Upgrade1Purchased = true;
                    else
                        business.Upgrade2Purchased = true;
                }
            
                world.DelEntity(eventEntity);
            }
        }
    }
}