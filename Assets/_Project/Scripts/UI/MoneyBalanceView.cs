using _Project.Scripts.Components;
using TMPro;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class MoneyBalanceView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _balance;
        private Wallet _wallet;

        public void Construct(Wallet wallet) => 
            _wallet = wallet;

        public void Init() => 
            _wallet.Money.Subscribe(UpdateBalanceText);

        private void UpdateBalanceText(float money) =>
            _balance.text = $"Balance: {money}$";
    }
}
