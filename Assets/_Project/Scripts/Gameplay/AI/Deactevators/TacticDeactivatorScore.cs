using System;
using UnityEngine;

namespace BattleBase.Gameplay.AI.Deactevators
{
    public abstract class TacticDeactivatorScore : ScriptableObject, ITacticDeactivatorScore
    {
        public abstract Type Type { get; }
    }
}