using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.PurchaseService
{
    [CreateAssetMenu(
        fileName = nameof(PurchaseConfig),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(PurchaseConfig))]
    public class PurchaseConfig : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private PurchaseType _type = PurchaseType.NonConsumable;
        [SerializeField] private Sprite _icon;
        [SerializeField][Min(0)] private int _coinsReward;

        public string Id => _id;

        public PurchaseType Type => _type;

        public Sprite Icon => _icon;

        public int CoinsReward => _coinsReward;

        public bool IsConsumable => _type == PurchaseType.Consumable;
    }
}