using _Project.Scripts.Components;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class BusinessView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _level;
        [SerializeField] private TextMeshProUGUI _income;
        [SerializeField] private TextMeshProUGUI _levelUpCost;
        [SerializeField] private Button _levelUp;
        [SerializeField] private Image _fill;
        [SerializeField] private GridLayoutGroup _grid;

        private Business _business;
        private Wallet _wallet;

        public GridLayoutGroup Grid => _grid;

        public void Construct(Business business, Wallet wallet)
        {
            _business = business;
            _wallet = wallet;
        }

        public void Init()
        {
            _name.text = _business.Data.Name;
            _business.Level.Subscribe(UpdateLevelText);
            _business.Income.Subscribe(UpdateIncomeText);
            _business.LevelUpCost.Subscribe(UpdateCostText);
            _business.IncomeProgress.Subscribe(UpdateIncomeProgress);
            _wallet.Money.Subscribe(UpdateLevelUpInteractability);
        }

        private void UpdateIncomeProgress(float incomeProgress) => 
            _fill.fillAmount = incomeProgress / _business.Data.IncomeDelay;

        private void UpdateLevelUpInteractability(float money) =>
            _levelUp.interactable = money >= _business.LevelUpCost.Value;

        private void UpdateCostText(float cost) =>
            _levelUpCost.text = $"Цена: {cost}$";

        private void UpdateLevelText(int level) =>
            _level.text = $"{level}";

        private void UpdateIncomeText(float income) =>
            _income.text = $"{income}";
    }
}