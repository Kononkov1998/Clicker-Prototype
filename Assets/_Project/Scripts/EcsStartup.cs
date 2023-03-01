using _Project.Scripts.Components;
using _Project.Scripts.Data;
using _Project.Scripts.Services.SaveLoad;
using _Project.Scripts.Systems;
using Leopotam.Ecs;
using UnityEngine;

namespace _Project.Scripts
{
    public sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField] private GameConfig _config;
        [SerializeField] private StaticData _staticData;

        private EcsWorld _world;
        private EcsSystems _systems;
        private EcsSystems _exitSystems;

        private ISaveLoadService _saveLoad;
        private RuntimeData _runtimeData;

        public void Start()
        {
            Application.targetFrameRate = 60;
            
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _exitSystems = new EcsSystems(_world);

            _runtimeData = new RuntimeData();
            _saveLoad = new SaveLoadService();

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif

            var saveSystem = new SaveSystem(_saveLoad, _runtimeData);
            _systems
                .Add(new LoadSystem(_world, _saveLoad, _runtimeData, _config))
                .Add(new CreateUiSystem(_staticData, _runtimeData))
                .Add(new TimerSystem<SaveEvent>())
                .Add(new BusinessProgressSystem())
                .Add(new BusinessIncomeSystem(_runtimeData))
                .Add(new BusinessBuyLevelSystem())
                .Add(new BusinessBuyImprovementSystem(_config))
                .Add(new BusinessLevelUpSystem())
                .Add(new WithdrawMoneySystem(_runtimeData))
                .Add(new UpdateBusinessLevelUpCostSystem())
                .Add(new UpdateBusinessIncomeSystem())
                .Add(new PeriodicSaveSystem(_world, _staticData))
                .Add(saveSystem)
                .OneFrame<LevelUpClickedEvent>()
                .OneFrame<BuyImprovementClickedEvent>()
                .OneFrame<SaveEvent>()
                .Init();

            _exitSystems
                .Add(saveSystem)
                .OneFrame<SaveEvent>()
                .Init();
        }

        private void Update() =>
            _systems?.Run();

#if UNITY_EDITOR
        private void OnApplicationQuit()
        {
            _world.NewEntity().Get<SaveEvent>();
            _exitSystems.Run();
        }
#else
        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                _world.NewEntity().Get<SaveEvent>();
                _exitSystems.Run();
            }
        }
#endif
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