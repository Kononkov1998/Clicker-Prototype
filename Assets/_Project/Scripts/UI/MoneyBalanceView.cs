using _Project.Scripts.Data;
using TMPro;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class MoneyBalanceView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _balance;
        private RuntimeData _runtimeData;

        public void Construct(RuntimeData runtimeData) => 
            _runtimeData = runtimeData;

        public void Init() => 
            _runtimeData.Money.Subscribe(UpdateBalanceText);

        private void UpdateBalanceText(float money) =>
            _balance.text = $"Balance: {Mathf.Floor(money)}$";
    }
}
