using UnityEngine;

namespace BattleBase.Gameplay.Actors.Colored
{
    public interface IColorGetter
    {
        public Color GetTeamColor(TeamType teamType);
    }
}