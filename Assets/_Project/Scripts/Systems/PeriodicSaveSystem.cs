using _Project.Scripts.Components;
using _Project.Scripts.Data;
using Leopotam.Ecs;

namespace _Project.Scripts.Systems
{
    public class PeriodicSaveSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly StaticData _staticData;

        private EcsFilter<TimerDoneEvent<SaveEvent>> _filter;

        public PeriodicSaveSystem(EcsWorld world, StaticData staticData)
        {
            _world = world;
            _staticData = staticData;
        }

        public void Init() =>
            _world.NewEntity().Get<Timer<SaveEvent>>().Value = _staticData.SaveInterval;

        public void Run()
        {
            foreach (int index in _filter)
            {
                EcsEntity entity = _filter.GetEntity(index);
                entity.Get<Timer<SaveEvent>>().Value = _staticData.SaveInterval;
                entity.Get<SaveEvent>();
            }
        }
    }
}