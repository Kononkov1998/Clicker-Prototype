using System;
using _Project.Scripts.Components;
using _Project.Scripts.Data;
using Leopotam.Ecs;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class BusinessImprovementView : MonoBehaviour
    {
        private const string IncomeTextTemplate = "Доход: +{0}%";
        private const string CostTextTemplate = "Цена: {0}$";
        private const string BoughtText = "Куплено";
        
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _income;
        [SerializeField] private TextMeshProUGUI _cost;
        [SerializeField] private Button _buy;

        private EcsEntity _businessEntity;
        private Business _business;
        private RuntimeData _runtimeData;
        private BusinessImprovement _improvement;
        private IDisposable _moneySubscription;

        private void OnDestroy() =>
            _buy.onClick.RemoveListener(OnBuyClicked);

        public void Construct(EcsEntity businessEntity, BusinessImprovement improvement, RuntimeData runtimeData)
        {
            _businessEntity = businessEntity;
            _business = _businessEntity.Get<Business>();
            _improvement = improvement;
            _runtimeData = runtimeData;
            _buy.onClick.AddListener(OnBuyClicked);
        }

        public void Init()
        {
            _name.text = _improvement.Name;
            _income.text = string.Format(IncomeTextTemplate, _improvement.MultiplierPercent);
            _cost.text = string.Format(CostTextTemplate, _improvement.Cost);
            if (_business.Improvements.Contains(_improvement.Id))
            {
                UpdateCostTextOnPurchase();
            }
            else
            {
                _business.Improvements.ObserveAdd().Subscribe(OnImprovementAdded);
                _moneySubscription = _runtimeData.Money.Subscribe(UpdateBuyInteractibility);
            }
        }

        private void OnBuyClicked() =>
            _businessEntity.Get<BuyImprovementClickedEvent>().Id = _improvement.Id;

        private void UpdateBuyInteractibility(float money) =>
            _buy.interactable = money >= _improvement.Cost;

        private void OnImprovementAdded(CollectionAddEvent<int> improvementId)
        {
            if (improvementId.Value == _improvement.Id)
            {
                _moneySubscription.Dispose();
                UpdateCostTextOnPurchase();
            }
        }

        private void UpdateCostTextOnPurchase()
        {
            _cost.text = BoughtText;
            _buy.interactable = false;
        }
    }
}