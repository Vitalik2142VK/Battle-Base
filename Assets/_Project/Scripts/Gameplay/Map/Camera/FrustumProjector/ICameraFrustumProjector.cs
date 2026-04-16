using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    public interface ICameraFrustumProjector
    {
        public ICameraArea Area { get; }

        public List<Vector3> ProjectCornersOntoPlaneFromPosition(Vector3 cameraPosition);
    }
}