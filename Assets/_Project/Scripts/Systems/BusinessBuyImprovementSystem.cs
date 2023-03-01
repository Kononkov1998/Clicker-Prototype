using _Project.Scripts.Components;
using _Project.Scripts.Data;
using Leopotam.Ecs;

namespace _Project.Scripts.Systems
{
    public class BusinessBuyImprovementSystem : IEcsRunSystem
    {
        private readonly GameConfig _config;
        private StaticData _staticData;

        private EcsFilter<Business, BuyImprovementClickedEvent> _businessFilter;

        public BusinessBuyImprovementSystem(GameConfig config) => 
            _config = config;

        public void Run()
        {
            foreach (int businessIndex in _businessFilter)
            {
                EcsEntity businessEntity = _businessFilter.GetEntity(businessIndex);
                ref Business business = ref businessEntity.Get<Business>();
                int improvementId = businessEntity.Get<BuyImprovementClickedEvent>().Id;
                BusinessImprovement improvement = _config.GetImprovement(business.Data.Id, improvementId);
                businessEntity.Get<WithdrawRequest>().Value = improvement.Cost;
                businessEntity.Get<UpdateIncomeRequest>();
                business.Improvements.Add(improvementId);
            }
        }
    }
}