using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{

    public interface IFrustumProjectionService
    {
        public void ProjectCornersOntoPlaneFromPosition(Vector3 cameraPosition, List<Vector3> outCorners);
    }
}