using _Project.Scripts.Data;
using TMPro;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class MoneyBalanceView : MonoBehaviour
    {
        private const string BalanceTextTemplate = "Balance: {0}$";
        
        [SerializeField] private TextMeshProUGUI _balance;
        private RuntimeData _runtimeData;

        public void Construct(RuntimeData runtimeData) =>
            _runtimeData = runtimeData;

        public void Init() =>
            _runtimeData.Money.Subscribe(UpdateBalanceText);

        private void UpdateBalanceText(float money) =>
            _balance.text = string.Format(BalanceTextTemplate, Mathf.Floor(money));
    }
}