using System.Collections.Generic;
using _Project.Scripts.Data;
using UniRx;

namespace _Project.Scripts.Components
{
    public struct Business
    {
        public BusinessData Data;
        public ReactiveProperty<int> Level;
        public ReactiveProperty<float> LevelUpCost;
        public ReactiveProperty<float> Income;
        public ReactiveProperty<float> IncomeProgress;
        public List<int> Improvements;
    }
}