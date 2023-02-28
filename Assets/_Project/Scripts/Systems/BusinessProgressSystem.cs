using _Project.Scripts.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace _Project.Scripts.Systems
{
    public class BusinessProgressSystem : IEcsRunSystem
    {
        private EcsFilter<Business, Active> _businessFilter;
        
        public void Run()
        {
            foreach (int businessIndex in _businessFilter)
            {
                EcsEntity businessEntity = _businessFilter.GetEntity(businessIndex);
                ref Business business = ref businessEntity.Get<Business>();
                
                if (business.IncomeProgress.Value >= business.Data.IncomeDelay)
                {
                    business.IncomeProgress.Value = 0f;
                    businessEntity.Get<GetIncomeRequest>();
                }
                
                business.IncomeProgress.Value += Time.deltaTime;
            }
        }
    }
}