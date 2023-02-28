using _Project.Scripts.Components;
using Leopotam.Ecs;

namespace _Project.Scripts.Systems
{
    public class BusinessIncomeSystem : IEcsRunSystem
    {
        private EcsFilter<Wallet> _wallet;
        private EcsFilter<Business, GetIncomeRequest> _businessFilter;
        
        public void Run()
        {
            ref Wallet wallet = ref _wallet.Get1(0);
            foreach (int businessIndex in _businessFilter)
            {
                EcsEntity businessEntity = _businessFilter.GetEntity(businessIndex);
                businessEntity.Del<GetIncomeRequest>();
                ref Business business = ref businessEntity.Get<Business>();
                wallet.Money.Value += business.Income.Value;
            }
        }
    }
}