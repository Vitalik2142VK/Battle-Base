using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors
{
    public interface IColored
    {
        public event Action<Color> ColorChanged;
    }
}
