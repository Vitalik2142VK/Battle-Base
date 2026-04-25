using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{

    public interface IFrustumProjectionService
    {
        public Vector3 ProjectedCenter { get; }

        public IReadOnlyList<Vector3> Corners { get; }

        public void ProjectCornersOntoPlaneFromPosition(Vector3 cameraPosition, List<Vector3> outCorners);
    }
}