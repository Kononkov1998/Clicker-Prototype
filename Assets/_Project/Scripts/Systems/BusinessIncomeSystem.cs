using _Project.Scripts.Components;
using _Project.Scripts.Data;
using Leopotam.Ecs;

namespace _Project.Scripts.Systems
{
    public class BusinessIncomeSystem : IEcsRunSystem
    {
        private readonly RuntimeData _runtimeData;
        private EcsFilter<Business, GetIncomeRequest> _businessFilter;

        public BusinessIncomeSystem(RuntimeData runtimeData) =>
            _runtimeData = runtimeData;

        public void Run()
        {
            foreach (int businessIndex in _businessFilter)
            {
                EcsEntity businessEntity = _businessFilter.GetEntity(businessIndex);
                businessEntity.Del<GetIncomeRequest>();
                ref Business business = ref businessEntity.Get<Business>();
                _runtimeData.Money.Value += business.Income.Value;
            }
        }
    }
}