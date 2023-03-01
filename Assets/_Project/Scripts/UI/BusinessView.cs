using _Project.Scripts.Components;
using _Project.Scripts.Data;
using Leopotam.Ecs;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class BusinessView : MonoBehaviour
    {
        private const string PriceTextTemplate = "Цена: {0}$";
        private const string IncomeTextTemplate = "{0}$";

        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _level;
        [SerializeField] private TextMeshProUGUI _income;
        [SerializeField] private TextMeshProUGUI _levelUpCost;
        [SerializeField] private Button _levelUp;
        [SerializeField] private Image _fill;
        [SerializeField] private GridLayoutGroup _grid;

        private EcsEntity _businessEntity;
        private Business _business;
        private RuntimeData _runtimeData;

        public GridLayoutGroup Grid => _grid;

        private void OnDestroy() => 
            _levelUp.onClick.RemoveListener(OnLevelUpClicked);

        public void Construct(EcsEntity businessEntity, RuntimeData runtimeData)
        {
            _businessEntity = businessEntity;
            _business = _businessEntity.Get<Business>();
            _runtimeData = runtimeData;
            _levelUp.onClick.AddListener(OnLevelUpClicked);
        }

        public void Init()
        {
            _name.text = _business.Data.Name;
            _business.Level.Subscribe(UpdateLevelText);
            _business.Income.Subscribe(UpdateIncomeText);
            _business.LevelUpCost.Subscribe(UpdateCostText);
            _business.LevelUpCost.Subscribe(UpdateLevelUpInteractability);
            _business.IncomeProgress.Subscribe(UpdateIncomeProgress);
            _runtimeData.Money.Subscribe(UpdateLevelUpInteractability);
        }

        private void OnLevelUpClicked() => 
            _businessEntity.Get<LevelUpClickedEvent>();

        private void UpdateIncomeProgress(float incomeProgress) => 
            _fill.fillAmount = incomeProgress / _business.Data.IncomeDelay;

        private void UpdateLevelUpInteractability(float _) => 
            _levelUp.interactable = _runtimeData.Money.Value >= _business.LevelUpCost.Value;

        private void UpdateCostText(float cost) =>
            _levelUpCost.text = string.Format(PriceTextTemplate, cost);

        private void UpdateLevelText(int level) =>
            _level.text = level.ToString();

        private void UpdateIncomeText(float income) =>
            _income.text = string.Format(IncomeTextTemplate, income);
    }
}