using System.Collections.Generic;
using _Project.Scripts.Components;
using _Project.Scripts.Data;
using Leopotam.Ecs;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Scripts.Systems
{
    public class SaveSystem : IEcsRunSystem
    {
        private EcsFilter<Wallet> _walletFilter;
        private EcsFilter<Business> _businessFilter;
        private EcsFilter<SaveRequest> _saveRequestFilter;

        public void Run()
        {
            foreach (int saveRequestIndex in _saveRequestFilter)
            {
                EcsEntity entity = _saveRequestFilter.GetEntity(saveRequestIndex);
                entity.Del<SaveRequest>();
                Save();
            }
        }

        private void Save()
        {
            var businesses = new List<Business>();
            foreach (int businessIndex in _businessFilter)
                businesses.Add(_businessFilter.Get1(businessIndex));

            var data = new PersistentData(_walletFilter.Get1(0), businesses);
            string json = JsonConvert.SerializeObject(data);
            PlayerPrefs.SetString("Progress", json);
        }
    }
}