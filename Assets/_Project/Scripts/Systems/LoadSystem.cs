using _Project.Scripts.Components;
using _Project.Scripts.Data;
using _Project.Scripts.Services.SaveLoad;
using Leopotam.Ecs;
using UniRx;

namespace _Project.Scripts.Systems
{
    public class LoadSystem : IEcsInitSystem
    {
        private readonly RuntimeData _runtimeData;
        private readonly EcsWorld _world;
        private readonly GameConfig _gameConfig;
        private readonly ISaveLoadService _saveLoad;

        public LoadSystem(EcsWorld world, ISaveLoadService saveLoad, RuntimeData runtimeData, GameConfig gameConfig)
        {
            _world = world;
            _saveLoad = saveLoad;
            _runtimeData = runtimeData;
            _gameConfig = gameConfig;
        }

        public void Init() =>
            Load();

        private void Load()
        {
            PersistentData data = _saveLoad.Load();
            LoadRuntimeData(data);
            CreateBusinesses(data);
        }

        private void LoadRuntimeData(PersistentData data) =>
            _runtimeData.Money.Value = data?.Money ?? 0f;

        private void CreateBusinesses(PersistentData data)
        {
            for (var i = 0; i < _gameConfig.Businesses.Count; i++)
            {
                BusinessData businessData = _gameConfig.Businesses[i];
                Business business = GetOrCreateDefaultBusiness(data, businessData);
                EcsEntity businessEntity = _world.NewEntity();

                businessEntity.Get<Business>() = business;

                if (i == 0 && data == null)
                    businessEntity.Get<LevelUpRequest>();

                if (business.Level.Value > 0)
                    businessEntity.Get<Active>();

                businessEntity.Get<UpdateIncomeRequest>();
                businessEntity.Get<UpdateLevelUpCostRequest>();
            }
        }

        private static Business GetOrCreateDefaultBusiness(PersistentData data, BusinessData businessData)
        {
            Business business;
            if (data != null && data.Businesses.ContainsKey(businessData.Id))
            {
                business = data.Businesses[businessData.Id];
                business.Data = businessData;
            }
            else
            {
                business = new Business
                {
                    Id = businessData.Id,
                    Data = businessData,
                    Improvements = new ReactiveCollection<int>(),
                    Level = new ReactiveProperty<int>(),
                    IncomeProgress = new ReactiveProperty<float>(),
                    LevelUpCost = new ReactiveProperty<float>(),
                    Income = new ReactiveProperty<float>()
                };
            }

            return business;
        }
    }
}