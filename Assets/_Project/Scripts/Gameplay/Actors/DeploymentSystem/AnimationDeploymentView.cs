using UnityEngine;

namespace BattleBase.Gameplay.Actors.DeploymentSystem
{
    public class AnimationDeploymentView : MonoBehaviour, IDeploymentView
    {
        private const string Idle = nameof(Idle);
        private const string Deploy = nameof(Deploy);

        [SerializeField] private Animator _animator;

        private IDeploymentEvets _evets;
        private int _hashDeploy;
        private int _hashIdle;

        private void Awake()
        {
            _hashIdle = Animator.StringToHash(Idle);
            _hashDeploy = Animator.StringToHash(Deploy);
        }

        private void OnEnable()
        {
            if (_evets == null)
                return;

            _evets.Finished += OnPlayIdle;
            _evets.Started += OnPlayDeploy;
        }

        private void OnDisable()
        {
            if (_evets == null)
                return;

            _evets.Finished -= OnPlayIdle;
            _evets.Started -= OnPlayDeploy;
        }

        public void Init(IDeploymentEvets deploymentEvets)
        {
            _evets = deploymentEvets ?? throw new System.ArgumentNullException(nameof(deploymentEvets));

            if (gameObject.activeSelf)
            {
                _evets.Finished += OnPlayIdle;
                _evets.Started += OnPlayDeploy;
            }
        }

        private void OnPlayIdle() =>
            _animator.SetTrigger(_hashIdle);

        private void OnPlayDeploy() =>
            _animator.SetTrigger(_hashDeploy);
    }
}
