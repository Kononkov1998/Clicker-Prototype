using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts.Data
{
    [CreateAssetMenu(menuName = "Config", fileName = "Config")]
    public class GameConfig : ScriptableObject
    {
        public List<BusinessData> Businesses;

        public BusinessImprovement GetImprovement(int businessId, int improvementId) => 
            Businesses.First(x => x.Id == businessId)
                .Improvements.First(x => x.Id == improvementId);

        private void OnValidate() =>
            RemoveDuplicatedIds();

        private void RemoveDuplicatedIds()
        {
            RemoveDuplicatedIds(Businesses);
            foreach (BusinessData business in Businesses)
                RemoveDuplicatedIds(business.Improvements);
        }

        private static void RemoveDuplicatedIds(IEnumerable<IIdentifiable> identifiables)
        {
            var improvementsIds = new List<int>();
            IEnumerable<IIdentifiable> identifiablesList = identifiables.ToList();

            foreach (IIdentifiable identifiable in identifiablesList)
            {
                if (improvementsIds.Contains(identifiable.Id))
                    identifiable.Id = GenerateNewId(identifiablesList);
                improvementsIds.Add(identifiable.Id);
            }
        }

        private static int GenerateNewId(IEnumerable<IIdentifiable> identifiables) =>
            identifiables.Max(x => x.Id) + 1;
    }
}