using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class GameUi : MonoBehaviour
    {
        [SerializeField] private MoneyBalanceView _moneyMoneyBalanceView;
        [SerializeField] private LayoutGroup _businessesViewsLayout;

        public MoneyBalanceView MoneyMoneyBalanceView => _moneyMoneyBalanceView;
        public LayoutGroup BusinessesViewsLayout => _businessesViewsLayout;
    }
}