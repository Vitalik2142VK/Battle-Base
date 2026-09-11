using UnityEngine;

namespace BattleBase.Gameplay.Actors.Building
{
    public class AnimationBuildingSiteView : MonoBehaviour, IBuildingSiteView
    {
        private const string DestroyBuild = nameof(DestroyBuild);

        [SerializeField] private Animator _animator;

        private IBuildingSiteEvents _events;
        private int _hashDestroyBuild;

        private void Awake()
        {
            _hashDestroyBuild = Animator.StringToHash(DestroyBuild);
        }

        private void OnEnable()
        {
            if (_events != null)
                _events.Showed += OnPlayDestroyBuild;
        }

        private void OnDisable()
        {
            if (_events != null)
                _events.Showed -= OnPlayDestroyBuild;
        }

        public void Init(IBuildingSiteEvents events)
        {
            _events = events ?? throw new System.ArgumentNullException(nameof(events));

            if (gameObject.activeSelf)
                _events.Showed += OnPlayDestroyBuild;
        }

        private void OnPlayDestroyBuild() =>
            _animator.SetTrigger(_hashDestroyBuild);
    }
}