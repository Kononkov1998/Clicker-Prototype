using _Project.Scripts.Components;
using Leopotam.Ecs;

namespace _Project.Scripts.Systems
{
    public class UpdateBusinessLevelUpCostSystem : IEcsRunSystem
    {
        private EcsFilter<Business, UpdateLevelUpCostRequest> _filter;

        public void Run()
        {
            foreach (int index in _filter)
            {
                EcsEntity entity = _filter.GetEntity(index);
                entity.Del<UpdateLevelUpCostRequest>();
                ref Business business = ref entity.Get<Business>();
                business.LevelUpCost.Value = (business.Level.Value + 1) * business.Data.BaseCost;
            }
        }
    }
}