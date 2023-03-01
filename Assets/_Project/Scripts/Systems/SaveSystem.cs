using System.Collections.Generic;
using _Project.Scripts.Components;
using _Project.Scripts.Data;
using _Project.Scripts.Services.SaveLoad;
using Leopotam.Ecs;

namespace _Project.Scripts.Systems
{
    public class SaveSystem : IEcsRunSystem
    {
        private readonly ISaveLoadService _saveLoad;
        private readonly RuntimeData _runtimeData;

        private EcsFilter<Business> _businessFilter;
        private EcsFilter<SaveEvent> _saveEventFilter;

        public SaveSystem(ISaveLoadService saveLoad, RuntimeData runtimeData)
        {
            _saveLoad = saveLoad;
            _runtimeData = runtimeData;
        }

        public void Run()
        {
            if (!_saveEventFilter.IsEmpty())
                Save();
        }

        private void Save()
        {
            var businesses = new Dictionary<int, Business>();
            foreach (int businessIndex in _businessFilter)
            {
                ref Business business = ref _businessFilter.Get1(businessIndex);
                businesses.Add(business.Id, business);
            }

            var data = new PersistentData
            {
                Money = _runtimeData.Money.Value,
                Businesses = businesses
            };
            _saveLoad.Save(data);
        }
    }
}