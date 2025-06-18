using _Project.Scripts.Data.Business;
using _Project.Scripts.Ecs.Components;
using Leopotam.EcsLite;

namespace _Project.Scripts.Ecs.Systems
{
    public class LevelUpSystem : IEcsRunSystem
    {
        private readonly AllBusinessData _allBusinessData;

        public LevelUpSystem(AllBusinessData allBusinessData)
        {
            _allBusinessData = allBusinessData;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var events = world.Filter<LevelUpEvent>().End();
            var walletFilter = world.Filter<Wallet>().End();
        
            if (walletFilter.GetEntitiesCount() == 0)
                return;
        
            var walletEntity = walletFilter.GetRawEntities()[0];
            ref var wallet = ref world.GetPool<Wallet>().Get(walletEntity);
            var eventPool = world.GetPool<LevelUpEvent>();
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
            
                var levelUpPrice = (business.Level + 1) * data.BaseCost;
            
                if (wallet.Balance >= levelUpPrice)
                {
                    wallet.Balance -= levelUpPrice;
                
                    business.Level++;
                    business.IsPurchased = true;
                }
            
                world.DelEntity(eventEntity);
            }
        }
    }
}