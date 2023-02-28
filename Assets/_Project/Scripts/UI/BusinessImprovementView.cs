using _Project.Scripts.Components;
using _Project.Scripts.Data;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class BusinessImprovementView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _income;
        [SerializeField] private TextMeshProUGUI _cost;
        [SerializeField] private Button _buy;
        private Business _business;
        private Wallet _wallet;
        private BusinessImprovement _improvement;

        public void Construct(Business business, BusinessImprovement improvement, Wallet wallet)
        {
            _business = business;
            _improvement = improvement;
            _wallet = wallet;
        }

        public void Init()
        {
            _name.text = _improvement.Name;
            _income.text = $"Доход: +{_improvement.MultiplierPercent}%";
            _cost.text = $"Цена: {_improvement.Cost}$";
            _business.Improvements.ToObservable().Subscribe(UpdateCostTextOnPurchase);
            _wallet.Money.Subscribe(UpdateBuyInteractibility);
        }

        private void UpdateBuyInteractibility(float money) => 
            _buy.interactable = money >= _improvement.Cost;

        private void UpdateCostTextOnPurchase(int improvementId)
        {
            if (improvementId == _improvement.Id)
                _cost.text = "Куплено";
        }
    }
}