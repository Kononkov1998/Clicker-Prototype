using UniRx;

namespace _Project.Scripts.Data
{
    public class RuntimeData
    {
        public readonly ReactiveProperty<float> Money;

        public RuntimeData() => 
            Money = new ReactiveProperty<float>();
    }
}