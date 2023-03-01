using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts.Data
{
    [Serializable]
    public class BusinessData : IIdentifiable
    {
        public string Name;
        public float IncomeDelay;
        public float BaseCost;
        public float BaseIncome;
        public List<BusinessImprovement> Improvements;

        [field: SerializeField, HideInInspector]
        public int Id { get; set; }

        public BusinessImprovement GetBusinessImprovement(int id) =>
            Improvements.FirstOrDefault(x => x.Id == id);
    }
}