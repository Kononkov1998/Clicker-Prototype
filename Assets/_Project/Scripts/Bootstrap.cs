using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private EcsStartup _ecsStartup;

        private void Start()
        {
            _ecsStartup.Init();
        }
    }
}
