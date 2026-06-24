using System.Collections;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.AI
{
    public class PlayerAI : MonoBehaviour
    {
        [SerializeField][Range(1f, 30f)] private float _timeTick = 2f;
        [SerializeField][Range(2f, 20f)] private float _timeBeforeStart = 2f;

        private IBrain _brain;
        private WaitForSeconds _sleepTime;

        private void Awake()
        {
            _sleepTime = new WaitForSeconds(_timeTick);
        }

        private void Start()
        {
            StartCoroutine(ActionAI());
        }

        [Inject]
        public void Construct(IBrain brain)
        {
            _brain = brain ?? throw new System.ArgumentNullException(nameof(brain));
        }

        private IEnumerator ActionAI()
        {
            yield return new WaitForSeconds(_timeBeforeStart);

            while (gameObject.activeSelf)
            {
                if (_brain.TryGetCommand(out ICommand command))
                    command.Execute();

                yield return _sleepTime;
            }
        }
    }
}