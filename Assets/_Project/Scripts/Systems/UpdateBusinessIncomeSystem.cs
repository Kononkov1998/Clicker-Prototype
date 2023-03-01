using _Project.Scripts.Components;
using Leopotam.Ecs;

namespace _Project.Scripts.Systems
{
    public class UpdateBusinessIncomeSystem : IEcsRunSystem
    {
        private EcsFilter<Business, UpdateIncomeRequest> _filter;

        public void Run()
        {
            foreach (int index in _filter)
            {
                EcsEntity entity = _filter.GetEntity(index);
                entity.Del<UpdateIncomeRequest>();
                ref Business business = ref entity.Get<Business>();

                var improvementsSum = 0f;
                foreach (int improvementId in business.Improvements)
                    improvementsSum += business.Data.GetBusinessImprovement(improvementId).MultiplierPercent / 100f;

                business.Income.Value = business.Level.Value * business.Data.BaseIncome * (1 + improvementsSum);
            }
        }
    }
}