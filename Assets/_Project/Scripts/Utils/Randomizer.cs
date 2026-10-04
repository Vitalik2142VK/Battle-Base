using UnityEngine;

namespace BattleBase.Utils
{
    public class Randomizer
    {
        public int GetRangeZero(int maxValue) =>
            Random.Range(0, maxValue);

        public float GetRangeZero(float maxValue) =>
            Random.Range(0, maxValue);

        public int GetRange(int minValue, int maxValue) =>
            Random.Range(minValue, maxValue);

        public float GetRange(float minValue, float maxValue) =>
            Random.Range(minValue, maxValue);
    }
}