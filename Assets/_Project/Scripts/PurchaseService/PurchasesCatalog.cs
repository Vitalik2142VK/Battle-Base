using System.Collections.Generic;
using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.PurchaseService
{
    [CreateAssetMenu(
        fileName = nameof(PurchasesCatalog),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(PurchasesCatalog))]
    public class PurchasesCatalog : ScriptableObject, IPurchaseCatalog
    {
        [SerializeField] private List<PurchaseConfig> _purchases = new();

        private Dictionary<string, PurchaseConfig> _byId;

        public IReadOnlyList<PurchaseConfig> All => _purchases;

        public PurchaseConfig GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;

            BuildIndexIfNeeded();

            return _byId.TryGetValue(id, out var config) ? config : null;
        }

        private void BuildIndexIfNeeded()
        {
            if (_byId != null)
                return;

            _byId = new Dictionary<string, PurchaseConfig>(_purchases.Count);

            foreach (var purchase in _purchases)
            {
                if (purchase == null || string.IsNullOrEmpty(purchase.Id))
                    continue;

                _byId[purchase.Id] = purchase;
            }
        }

#if UNITY_EDITOR
        private void OnValidate() => _byId = null;
#endif
    }
}