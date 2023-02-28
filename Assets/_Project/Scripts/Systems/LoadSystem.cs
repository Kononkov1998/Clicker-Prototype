using System.Collections.Generic;
using _Project.Scripts.Components;
using _Project.Scripts.Data;
using Leopotam.Ecs;
using Newtonsoft.Json;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.Systems
{
    public class LoadSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world;
        private readonly GameConfig _gameConfig;

        private EcsFilter<Wallet> _wallet;

        public LoadSystem(EcsWorld world, GameConfig gameConfig)
        {
            _world = world;
            _gameConfig = gameConfig;
        }

        public void Init()
        {
            Load();
        }

        private void Load()
        {
            string json = PlayerPrefs.GetString("Progress");
            var data = JsonConvert.DeserializeObject<PersistentData>(json);

            CreateWallet(data);
            CreateBusinesses(data);
        }

        private void CreateWallet(PersistentData data)
        {
            Wallet wallet = data?.Wallet ?? new Wallet {Money = new ReactiveProperty<float>()};
            EcsEntity walletEntity = _world.NewEntity();
            walletEntity.Get<Wallet>() = wallet;
        }

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
            }
        }

        private static Business GetOrCreateDefaultBusiness(PersistentData data, BusinessData businessData)
        {
            Business business;
            if (data != null && data.HasBusiness(businessData.Id))
            {
                business = data.GetBusiness(businessData.Id);
            }
            else
            {
                business = new Business
                {
                    Data = businessData,
                    Improvements = new List<int>(),
                    Level = new ReactiveProperty<int>(0),
                    IncomeProgress = new ReactiveProperty<float>()
                };
                business.LevelUpCost = new ReactiveProperty<float>(GameConfig.CalculateLevelUpCost(business));
                business.Income = new ReactiveProperty<float>(GameConfig.CalculateIncome(business));
            }

            return business;
        }
    }
}