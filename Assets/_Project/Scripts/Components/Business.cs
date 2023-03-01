using System;
using _Project.Scripts.Data;
using UniRx;

namespace _Project.Scripts.Components
{
    public struct Business
    {
        [NonSerialized] public BusinessData Data;
        public int Id;
        public ReactiveProperty<int> Level;
        public ReactiveProperty<float> LevelUpCost;
        public ReactiveProperty<float> Income;
        public ReactiveProperty<float> IncomeProgress;
        public ReactiveCollection<int> Improvements;
    }
}