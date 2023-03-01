using _Project.Scripts.Components;
using _Project.Scripts.Data;
using Leopotam.Ecs;

namespace _Project.Scripts.Systems
{
    public class WithdrawMoneySystem : IEcsRunSystem
    {
        private readonly RuntimeData _runtimeData;
        private EcsFilter<WithdrawRequest> _withdrawFilter;

        public WithdrawMoneySystem(RuntimeData runtimeData) => 
            _runtimeData = runtimeData;

        public void Run()
        {
            foreach (int index in _withdrawFilter)
            {
                EcsEntity entity = _withdrawFilter.GetEntity(index);
                ref WithdrawRequest withdrawRequest = ref entity.Get<WithdrawRequest>();
                _runtimeData.Money.Value -= withdrawRequest.Value;
                entity.Del<WithdrawRequest>();
            }
        }
    }
}