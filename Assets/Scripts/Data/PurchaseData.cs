using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Data {
    [Serializable]
    public class PurchaseData {
        public List<BoughtIAP> BoughtIAPs = new List<BoughtIAP>();

        public event Action Changed;
        
        public void AddPurchase(string id) {
            var boughtIAP = GetBoughtIAPById(id);

            if ( boughtIAP != null ) {
                boughtIAP.Count++;
            }
            else {
                BoughtIAPs.Add(new BoughtIAP {Id = id, Count = 1});
            }
            
            Changed?.Invoke();
        }

        [CanBeNull]
        public BoughtIAP GetBoughtIAPById(string id) {
            foreach (var boughtIAP in BoughtIAPs) {
                if ( boughtIAP.Id == id ) {
                    return boughtIAP;
                }
            }

            return null;
        }
    }
}