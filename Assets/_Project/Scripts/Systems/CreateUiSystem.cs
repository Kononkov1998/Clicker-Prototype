using _Project.Scripts.Components;
using _Project.Scripts.Data;
using _Project.Scripts.UI;
using Leopotam.Ecs;
using UnityEngine;

namespace _Project.Scripts.Systems
{
    public class CreateUiSystem : IEcsInitSystem
    {
        private readonly StaticData _staticData;
        private readonly RuntimeData _runtimeData;

        private EcsFilter<Business> _businessFilter;

        public CreateUiSystem(StaticData staticData, RuntimeData runtimeData)
        {
            _staticData = staticData;
            _runtimeData = runtimeData;
        }

        public void Init()
        {
            GameUi gameUi = CreateGameUi();
            CreateBusinessViews(gameUi);
        }

        private GameUi CreateGameUi()
        {
            GameUi gameUi = Object.Instantiate(_staticData.GameUiPrefab);
            gameUi.MoneyMoneyBalanceView.Construct(_runtimeData);
            gameUi.MoneyMoneyBalanceView.Init();
            return gameUi;
        }

        private void CreateBusinessViews(GameUi gameUi)
        {
            foreach (int businessIndex in _businessFilter)
            {
                EcsEntity businessEntity = _businessFilter.GetEntity(businessIndex);
                BusinessView businessView = Object.Instantiate(
                    _staticData.BusinessViewPrefab,
                    gameUi.BusinessesViewsLayout.transform
                );
                businessView.Construct(businessEntity, _runtimeData);
                businessView.Init();

                CreateImprovementsViews(businessEntity, businessView.Grid.transform);
            }
        }

        private void CreateImprovementsViews(EcsEntity businessEntity, Transform parent)
        {
            foreach (BusinessImprovement improvement in businessEntity.Get<Business>().Data.Improvements)
            {
                BusinessImprovementView improvementView = Object.Instantiate(
                    _staticData.BusinessImprovementViewPrefab,
                    parent
                );
                improvementView.Construct(businessEntity, improvement, _runtimeData);
                improvementView.Init();
            }
        }
    }
}