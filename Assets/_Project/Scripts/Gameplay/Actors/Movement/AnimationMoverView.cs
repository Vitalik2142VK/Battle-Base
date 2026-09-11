using UnityEngine;

namespace BattleBase.Gameplay.Actors.Movement
{
    public class AnimationMoverView : MonoBehaviour, IMoverViewComponent
    {
        private const string IsMoving = nameof(IsMoving);

        [SerializeField] private Animator _animator;

        private IMoverEvents _evets;
        private int _hashIsMoving;
        private bool _isMoving;

        private void Awake()
        {
            _hashIsMoving = Animator.StringToHash(IsMoving);
        }

        private void OnEnable()
        {
            if (_evets == null)
                return;

            _evets.Moved += OnPlayMove;
            _evets.Stoped += OnPlayStop;
            _isMoving = false;
        }

        private void OnDisable()
        {
            if (_evets == null)
                return;

            _evets.Moved -= OnPlayMove;
            _evets.Stoped -= OnPlayStop;
        }

        public void Init(IMoverEvents moverEvents)
        {
            _evets = moverEvents ?? throw new System.ArgumentNullException(nameof(moverEvents));

            if (gameObject.activeSelf)
            {
                _evets.Moved += OnPlayMove;
                _evets.Stoped += OnPlayStop;
            }
        }

        private void OnPlayMove()
        {
            if (_isMoving)
                return;

            _isMoving = true;
            _animator.SetBool(_hashIsMoving, _isMoving);
        }

        private void OnPlayStop()
        {
            if (_isMoving == false)
                return;

            _isMoving = false;
            _animator.SetBool(_hashIsMoving, _isMoving);
        }
    }
}
