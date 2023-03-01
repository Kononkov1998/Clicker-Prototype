using System;
using UnityEngine;

namespace _Project.Scripts.Data
{
    [Serializable]
    public class BusinessImprovement : IIdentifiable
    {
        public string Name;
        public float Cost;
        public float MultiplierPercent;

        [field: SerializeField, HideInInspector]
        public int Id { get; set; }
    }
}