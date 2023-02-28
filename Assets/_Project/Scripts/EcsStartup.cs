using _Project.Scripts.Data;
using _Project.Scripts.Systems;
using _Project.Scripts.UI;
using Leopotam.Ecs;
using UnityEngine;

namespace _Project.Scripts
{
    public sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField] private GameUi _gameUi;
        [SerializeField] private GameConfig _config;
        [SerializeField] private StaticData _staticData;

        private EcsWorld _world;
        private EcsSystems _systems;

        public void Init()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
            _systems
                .Add(new LoadSystem(_world, _config))
                .Add(new CreateUiSystem(_gameUi, _staticData))
                .Add(new BusinessProgressSystem())
                .Add(new BusinessIncomeSystem())
                .Add(new BusinessLevelUpSystem())
                .Add(new SaveSystem())
                .Add(new DebugSystem(_world))
                .Init();
        }

        private void Update() =>
            _systems?.Run();

        private void OnDestroy()
        {
            if (_systems == null) return;

            _systems.Destroy();
            _systems = null;
            _world.Destroy();
            _world = null;
        }
    }
}