using _Project.Scripts.Components;
using _Project.Scripts.Data;
using _Project.Scripts.UI;
using Leopotam.Ecs;
using UnityEngine;

namespace _Project.Scripts.Systems
{
    public class CreateUiSystem : IEcsInitSystem
    {
        private readonly GameUi _gameUi;
        private readonly StaticData _staticData;

        private EcsFilter<Business> _businessFilter;
        private EcsFilter<Wallet> _walletFilter;

        public CreateUiSystem(GameUi gameUi, StaticData staticData)
        {
            _gameUi = gameUi;
            _staticData = staticData;
        }

        public void Init()
        {
            ref Wallet wallet = ref _walletFilter.Get1(0);
            CreateMoneyBalanceView(wallet);
            CreateBusinessViews(wallet);
        }

        private void CreateMoneyBalanceView(Wallet wallet)
        {
            _gameUi.MoneyMoneyBalanceView.Construct(wallet);
            _gameUi.MoneyMoneyBalanceView.Init();
        }

        private void CreateBusinessViews(Wallet wallet)
        {
            foreach (int businessIndex in _businessFilter)
            {
                ref Business business = ref _businessFilter.Get1(businessIndex);
                BusinessView businessView = Object.Instantiate(
                    _staticData.BusinessViewPrefab,
                    _gameUi.BusinessesViewsLayout.transform
                );
                businessView.Construct(business, wallet);
                businessView.Init();

                CreateImprovementsViews(business, businessView.Grid.transform, wallet);
            }
        }

        private void CreateImprovementsViews(Business business, Transform parent, Wallet wallet)
        {
            foreach (BusinessImprovement improvement in business.Data.Improvements)
            {
                BusinessImprovementView improvementView = Object.Instantiate(
                    _staticData.BusinessImprovementViewPrefab,
                    parent
                );
                improvementView.Construct(business, improvement, wallet);
                improvementView.Init();
            }
        }
    }
}