using UnityEngine;

namespace BattleBase.Gameplay.Actors.Spawn.View
{
    public class AnimationSpawnerView : MonoBehaviour, IActorSpawnerView
    {
        private const string StartSpawn = nameof(StartSpawn);
        private const string CancleSpawn = nameof(CancleSpawn);
        private const string FinishSpawn = nameof(FinishSpawn);

        [SerializeField] private Animator _animator;

        private IActorSpawnerNotifier _notifier;
        private int _hashStartSpawn;
        private int _hashFinishSpawn;
        private int _hashCancleSpawn;

        private void Awake()
        {
            _hashStartSpawn = Animator.StringToHash(StartSpawn);
            _hashCancleSpawn = Animator.StringToHash(CancleSpawn);
            _hashFinishSpawn = Animator.StringToHash(FinishSpawn);
        }

        private void OnEnable()
        {
            if (_notifier == null)
                return;

            _notifier.SpawnStarted += OnPlayStartBuild;
            _notifier.SpawnCancled += OnPlayCancleBuild;
            _notifier.SpawnFinished += OnPlayFinishBuild;
        }

        private void OnDisable()
        {
            if (_notifier == null)
                return;

            _notifier.SpawnStarted -= OnPlayStartBuild;
            _notifier.SpawnCancled -= OnPlayCancleBuild;
            _notifier.SpawnFinished -= OnPlayFinishBuild;
        }

        public void Init(IActorSpawnerNotifier notifier)
        {
            _notifier = notifier ?? throw new System.ArgumentNullException(nameof(notifier));

            if (gameObject.activeSelf)
            {
                _notifier.SpawnStarted += OnPlayStartBuild;
                _notifier.SpawnCancled += OnPlayCancleBuild;
                _notifier.SpawnFinished += OnPlayFinishBuild;
            }
        }

        private void OnPlayStartBuild() => 
            _animator.SetTrigger(_hashStartSpawn);

        private void OnPlayCancleBuild() =>
            _animator.SetTrigger(_hashCancleSpawn);

        private void OnPlayFinishBuild() =>
            _animator.SetTrigger(_hashFinishSpawn);
    }
}
