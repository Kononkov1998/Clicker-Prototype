using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.Data
{
    [CreateAssetMenu(menuName = "Static Data", fileName = "StaticData")]
    public class StaticData : ScriptableObject
    {
        public float SaveInterval;
        public GameUi GameUiPrefab;
        public BusinessView BusinessViewPrefab;
        public BusinessImprovementView BusinessImprovementViewPrefab;
    }
}