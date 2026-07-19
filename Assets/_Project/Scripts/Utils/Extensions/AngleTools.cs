namespace BattleBase.Utils.Extensions
{
    public static class AngleTools
    {
        public static float NormalizeAngle(float angle)
        {
            if (angle > 180f)
                angle -= 360f;

            return angle;
        }
    }
}