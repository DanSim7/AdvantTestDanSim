using _Project.Scripts.Data.Business;
using _Project.Scripts.Ecs.Components;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.Diagnostics;

namespace _Project.Scripts.Ecs.Systems
{
    public class BusinessProgressSystem : IEcsRunSystem
    {
        private readonly AllBusinessData _allBusinessData;

        public BusinessProgressSystem(AllBusinessData allBusinessData)
        {
            _allBusinessData = allBusinessData;
        }
        
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
        
            var businessFilter = world.Filter<Business>().End();
            var walletFilter = world.Filter<Wallet>().End();
        
            if (walletFilter.GetEntitiesCount() == 0)
                return;
        
            var walletEntity = walletFilter.GetRawEntities()[0];
            ref var wallet = ref world.GetPool<Wallet>().Get(walletEntity);
            var businessPool = world.GetPool<Business>();
        
            foreach (var entity in businessFilter)
            {
                ref var business = ref businessPool.Get(entity);
            
                if (!business.IsPurchased)
                    continue;
            
                var data = _allBusinessData.BusinessDatas[business.ID];
            
                business.Progress += Time.deltaTime / data.IncomeDelay;
            
                if (business.Progress >= 1f)
                {
                    var income = Utilities.Utils.CalculateIncome(business, data);
                    wallet.Balance += income;
                
                    business.Progress -= 1f;
                }
            }
        }
    }
}