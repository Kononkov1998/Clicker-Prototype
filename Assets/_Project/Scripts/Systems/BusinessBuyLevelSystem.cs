using _Project.Scripts.Components;
using Leopotam.Ecs;

namespace _Project.Scripts.Systems
{
    public class BusinessBuyLevelSystem : IEcsRunSystem
    {
        private EcsFilter<Business, LevelUpClickedEvent> _businessFilter;

        public void Run()
        {
            foreach (int businessIndex in _businessFilter)
            {
                EcsEntity businessEntity = _businessFilter.GetEntity(businessIndex);
                ref Business business = ref businessEntity.Get<Business>();
                businessEntity.Get<WithdrawRequest>().Value = business.LevelUpCost.Value;
                businessEntity.Get<LevelUpRequest>();
            }
        }
    }
}