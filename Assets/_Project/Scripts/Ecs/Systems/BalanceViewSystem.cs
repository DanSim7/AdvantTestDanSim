using _Project.Scripts.Ecs.Components;
using Leopotam.EcsLite;
using TMPro;

namespace _Project.Scripts.Ecs.Systems
{
    public class BalanceViewSystem : IEcsRunSystem
    {
        private readonly TextMeshProUGUI _balanceText;

        public BalanceViewSystem(TextMeshProUGUI balanceText)
        {
            _balanceText = balanceText;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<Wallet>().End();
        
            foreach (var entity in filter)
            {
                ref var wallet = ref world.GetPool<Wallet>().Get(entity);
                _balanceText.text = $"Баланс: {wallet.Balance:F0}$";
            }
        }
    }
}