using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Multiple
{
    public class MultyAttackerViewComponent : MonoBehaviour, IMultyAttackerViewComponent
    {
        [SerializeField] private AttackerView[] _additionalAttackerView;

        public IEnumerable<IAttackerViewComponent> AdditionalAttackerView => _additionalAttackerView;
    }
}