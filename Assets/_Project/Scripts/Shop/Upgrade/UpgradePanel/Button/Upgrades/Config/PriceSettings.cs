using UnityEngine;

namespace BattleBase.ShopSystem
{
    [System.Serializable]
    public class PriceSettings
    {
        [SerializeField] private float _growthRate;
        [SerializeField] private float _decay;

        public float GrowthRate => _growthRate;

        public float Decay => _decay;
    }
}