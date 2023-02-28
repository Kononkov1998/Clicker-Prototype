using _Project.Scripts.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace _Project.Scripts.Systems
{
    public class DebugSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;

        public DebugSystem(EcsWorld world) => 
            _world = world;

        public void Run()
        {
            if (Input.GetKeyDown(KeyCode.S)) 
                _world.NewEntity().Get<SaveRequest>();
        }
    }
}