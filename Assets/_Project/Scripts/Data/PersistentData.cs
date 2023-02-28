using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Components;

namespace _Project.Scripts.Data
{
    public class PersistentData
    {
        public Wallet Wallet;
        public List<Business> Businesses;

        public PersistentData(Wallet wallet, List<Business> businesses)
        {
            Wallet = wallet;
            Businesses = businesses;
        }

        public bool HasBusiness(int id) => 
            Businesses.Any(x => x.Data.Id == id);

        public Business GetBusiness(int id) => 
            Businesses.FirstOrDefault(x=>x.Data.Id == id);
    }
}