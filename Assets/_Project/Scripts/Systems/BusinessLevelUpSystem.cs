using _Project.Scripts.Components;
using _Project.Scripts.Data;
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
                business.Income.Value = GameConfig.CalculateIncome(business);
                business.LevelUpCost.Value = GameConfig.CalculateLevelUpCost(business);

                if (previousLevel == 0)
                    businessEntity.Get<Active>();
            }
        }
    }
}