using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{

    public interface IFrustumProjectionService
    {
        public IReadOnlyList<Vector3> Corners { get; }

        public Vector3 ProjectedCenter { get; }

        public void ProjectCornersOntoPlaneFromPosition(Vector3 cameraPosition, List<Vector3> outCorners);
    }
}