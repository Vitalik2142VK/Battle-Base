using System;
using UnityEngine;

namespace BattleBase.Gameplay.CameraNavigation
{
    public readonly struct FrustumProjection : IEquatable<FrustumProjection>
    {
        public FrustumProjection(
            Vector3 leftUp,
            Vector3 leftDown,
            Vector3 rightUp,
            Vector3 rightDown,
            Vector3 center,
            float bottomWidth,
            float topWidth,
            float leftHeight,
            float rightHeight)
        {
            LeftUp = leftUp;
            LeftDown = leftDown;
            RightUp = rightUp;
            RightDown = rightDown;
            Center = center;
            BottomWidth = bottomWidth;
            TopWidth = topWidth;
            LeftHeight = leftHeight;
            RightHeight = rightHeight;
        }

        public Vector3 LeftUp { get; }

        public Vector3 LeftDown { get; }

        public Vector3 RightUp { get; }

        public Vector3 RightDown { get; }

        public Vector3 Center { get; }

        public float BottomWidth { get; }

        public float TopWidth { get; }

        public float LeftHeight { get; }

        public float RightHeight { get; }

        public bool Equals(FrustumProjection other)
        {
            const float eps = 1e-5f;

            return Vector3.Distance(LeftUp, other.LeftUp) < eps &&
                   Vector3.Distance(LeftDown, other.LeftDown) < eps &&
                   Vector3.Distance(RightUp, other.RightUp) < eps &&
                   Vector3.Distance(RightDown, other.RightDown) < eps &&
                   Vector3.Distance(Center, other.Center) < eps &&
                   Mathf.Abs(BottomWidth - other.BottomWidth) < eps &&
                   Mathf.Abs(TopWidth - other.TopWidth) < eps &&
                   Mathf.Abs(LeftHeight - other.LeftHeight) < eps &&
                   Mathf.Abs(RightHeight - other.RightHeight) < eps;
        }

        public override bool Equals(object obj) =>
            obj is FrustumProjection other && Equals(other);

        public override int GetHashCode()
        {
            int hash = HashCode.Combine(LeftUp, LeftDown, RightUp, RightDown, Center, BottomWidth, TopWidth);
            hash = HashCode.Combine(hash, LeftHeight, RightHeight);

            return hash;
        }

        public static bool operator ==(FrustumProjection left, FrustumProjection right) =>
            left.Equals(right);

        public static bool operator !=(FrustumProjection left, FrustumProjection right) =>
            left.Equals(right) == false;
    }
}