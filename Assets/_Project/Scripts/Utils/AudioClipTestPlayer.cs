using System.Collections;
using UnityEngine;

namespace BattleBase.Utils
{
    //todo remove for release
    public class AudioClipTestPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField][Min(0.001f)] private float _playbackTime = 1f;

        private void Start()
        {
            StartCoroutine(PlayClip());
        }

        private IEnumerator PlayClip()
        {
            while (gameObject.activeSelf)
            {
                _audioSource.Play();

                yield return new WaitForSeconds(_playbackTime);
            }
        }
    }
}