using BattleBase.Localization;
using BattleBase.Utils;
using UnityEngine;

namespace BattleBase.Gameplay.Units
{
    [CreateAssetMenu(
    fileName = nameof(UnitData),
    menuName = Constants.ConfigsAssetMenuPath + nameof(UnitConfig) + "/" + nameof(UnitData))]
    public class UnitData : ScriptableObject, IUnitData
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private LanguageTextsSet _name;
        [SerializeField] private LanguageTextsSet _description;

        public Sprite Icon => _icon;

        public ILanguageVisitor Name => _name;

        public ILanguageVisitor Description => _description;
    }
}
