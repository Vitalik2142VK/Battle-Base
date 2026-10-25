using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

namespace BattleBase.PurchaseService
{
    public class YandexGamePurchaseAdapter : IPurchaseService, IDisposable
    {
        private readonly IPurchaseCatalog _catalog;
        private readonly HashSet<string> _consumingIds = new();

        public YandexGamePurchaseAdapter(IPurchaseCatalog catalog)
        {
            _catalog = catalog ?? throw new ArgumentNullException(nameof(catalog));

            YG2.onPurchaseSuccess += OnPurchaseSuccess;
        }

        public event Action<PurchaseConfig> PurchaseCompleted;

        public void Buy(PurchaseConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            YG2.BuyPayments(config.Id);
        }

        public void RestorePendingPurchases()
        {
            if (YG2.isSDKEnabled == false)
                return;

            foreach (var config in _catalog.All)
            {
                if (config == null || config.IsConsumable == false)
                    continue;

                if (YG2.PurchaseByID(config.Id) != null)
                    ConsumeAndDispatch(config);
            }
        }

        public void Dispose() =>
            YG2.onPurchaseSuccess -= OnPurchaseSuccess;

        private void OnPurchaseSuccess(string id)
        {
            PurchaseConfig config = _catalog.GetById(id);
            if (config == null)
            {
                Debug.LogError($"[Purchase] Unknown purchase id received from SDK: '{id}'");
                return;
            }

            if (config.IsConsumable)
            {
                if (_consumingIds.Contains(id))
                {
                    _consumingIds.Remove(id);
                    PurchaseCompleted?.Invoke(config);
                }
                else
                {
                    ConsumeAndDispatch(config);
                }
            }
            else
            {
                PurchaseCompleted?.Invoke(config);
            }
        }

        private void ConsumeAndDispatch(PurchaseConfig config)
        {
            _consumingIds.Add(config.Id);
            YG2.ConsumePurchaseByID(config.Id, true);
        }
    }
}