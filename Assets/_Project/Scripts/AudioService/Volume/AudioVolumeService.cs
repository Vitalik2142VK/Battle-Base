using System;
using BattleBase.Utils.Constants;
using UnityEngine.Audio;

namespace BattleBase.AudioService
{
    public class AudioVolumeService : IDisposable
    {
        private readonly AudioMixer _mixer;
        private readonly AudioVolumeModel _model;

        public AudioVolumeService(AudioMixer mixer, AudioVolumeModel model)
        {
            _mixer = mixer != null ? mixer : throw new ArgumentNullException(nameof(mixer));
            _model = model ?? throw new ArgumentNullException(nameof(model));

            _model.Changed += UpdateVolume;
        }

        public void Dispose() =>
            _model.Changed -= UpdateVolume;

        public void UpdateVolume()
        {
            AudioVolumeSetter.SetNormalizedVolume(_mixer, AudioMixerGroupNames.General, _model.General);
            AudioVolumeSetter.SetNormalizedVolume(_mixer, AudioMixerGroupNames.Music, _model.Music);
            AudioVolumeSetter.SetNormalizedVolume(_mixer, AudioMixerGroupNames.Sfx, _model.Sfx);
        }
    }
}