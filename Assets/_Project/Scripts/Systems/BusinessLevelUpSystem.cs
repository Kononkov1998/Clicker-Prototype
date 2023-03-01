using _Project.Scripts.Components;
using Leopotam.Ecs;

namespace _Project.Scripts.Systems
{
    public class BusinessLevelUpSystem : IEcsRunSystem
    {
        private EcsFilter<Business, LevelUpRequest> _businessFilter;

        public void Run()
        {
            foreach (int businessIndex in _businessFilter)
            {
                EcsEntity businessEntity = _businessFilter.GetEntity(businessIndex);
                businessEntity.Del<LevelUpRequest>();

                ref Business business = ref businessEntity.Get<Business>();
                int previousLevel = business.Level.Value;
                business.Level.Value += 1;
                businessEntity.Get<UpdateIncomeRequest>();
                businessEntity.Get<UpdateLevelUpCostRequest>();

                if (previousLevel == 0)
                    businessEntity.Get<Active>();
            }
        }
    }
}