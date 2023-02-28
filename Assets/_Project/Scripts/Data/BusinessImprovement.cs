using System;

namespace _Project.Scripts.Data
{
    [Serializable]
    public class BusinessImprovement : IIdentifiable
    {
        public string Name;
        public float Cost;
        public float MultiplierPercent;
        public int Id { get; set; }
    }
}