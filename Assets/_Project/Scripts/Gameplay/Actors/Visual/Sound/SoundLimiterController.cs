using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Visual.Sound
{
    public class SoundLimiterController : MonoBehaviour, ISoundLimiterController
    {
        [SerializeField] private List<SoundPlaybackLimiter> _limiters;

        private Dictionary<string, SoundPlaybackLimiter> _sotrLimiters;

        private void OnValidate()
        {
            SoundPlaybackLimiter[] limiters = transform.GetComponentsInChildren<SoundPlaybackLimiter>();

            foreach (var limiter in limiters)
            {
                if (_limiters.Contains(limiter) == false)
                    _limiters.Add(limiter);
            }

            if (_limiters.Count == 0)
                return;

            for (int i = 0; i < _limiters.Count; i++)
            {
                if (_limiters[i] == null)
                    _limiters.RemoveAt(i--);
            }
        }

        private void Awake()
        {
            _sotrLimiters = new Dictionary<string, SoundPlaybackLimiter>();

            foreach (var limiter in _limiters)
                _sotrLimiters.Add(limiter.SoundEffectId, limiter);
        }

        public bool CanPlaySound(string soundEffectId)
        {
            if (string.IsNullOrEmpty(soundEffectId))
                throw new ArgumentException($"{nameof(soundEffectId)} cannot be null or empty");

            if (_sotrLimiters.TryGetValue(soundEffectId, out var limiter) == false)
                throw new InvalidOperationException($"{_sotrLimiters} don't contains key '{soundEffectId}'");

            return limiter.CanPlaySound;
        }
    }
}