using System;
using UnityEngine;
using UnityEngine.Audio;

namespace BattleBase.AudioService
{
    public static class AudioVolumeSetter
    {
        private const float MinimumLevel = -80f;
        private const float MaximumLevel = 20f;

        public static void SetNormalizedVolume(AudioMixer mixer, string group, float normalized)
        {
            if (mixer == null)
                throw new ArgumentNullException(nameof(mixer));

            if (string.IsNullOrEmpty(group))
                throw new ArgumentNullException(nameof(group));

            normalized = Mathf.Clamp01(normalized);
            float level = ConvertNormalizedToLevel(normalized);
            mixer.SetFloat(group, level);
        }

        private static float ConvertNormalizedToLevel(float normalized)
        {
            if (normalized <= 0f)
                return MinimumLevel;

            float level = Mathf.Log10(normalized) * MaximumLevel;

            return Mathf.Clamp(level, MinimumLevel, 0f);
        }
    }
}