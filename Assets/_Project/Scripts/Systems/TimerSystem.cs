using _Project.Scripts.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace _Project.Scripts.Systems
{
    public class TimerSystem<T> : IEcsRunSystem where T : struct
    {
        private EcsFilter<Timer<T>> _timerFilter;
        private EcsFilter<TimerDoneEvent<T>> _doneFilter;
    
        public void Run()
        {
            foreach (int idx in _doneFilter)
                _doneFilter.GetEntity(idx).Del<TimerDoneEvent<T>>();
    
            foreach (int idx in _timerFilter)
            {
                ref Timer<T> timer = ref _timerFilter.Get1(idx);
                timer.Value -= Time.deltaTime;
                if (timer.Value <= 0)
                {
                    _timerFilter.GetEntity(idx).Get<TimerDoneEvent<T>>();
                    _timerFilter.GetEntity(idx).Del<Timer<T>>();
                }
            }
        }
    }
}