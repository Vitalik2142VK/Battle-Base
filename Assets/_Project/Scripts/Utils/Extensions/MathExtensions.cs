namespace BattleBase.Utils.Extensions
{
    public static class MathExtensions
    {
        public static float Remap(this float value, float inMin, float inMax, float outMin, float outMax)
        {
            float t = (value - inMin) / (inMax - inMin);

            return outMin + t * (outMax - outMin);
        }
    }
}