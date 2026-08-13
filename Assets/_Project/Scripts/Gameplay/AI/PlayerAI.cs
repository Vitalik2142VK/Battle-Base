using BattleBase.Core;
using BattleBase.Utils;
using System.Collections;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.AI
{
    public class PlayerAI : MonoBehaviour
    {
        [SerializeField][Range(1f, 30f)] private float _defaultTimeTick = 2f;
        [SerializeField][Range(2f, 20f)] private float _timeBeforeStart = 2f;

        private IBrain _brain;
        private WaitForSeconds _sleepTime;
        private float _timeTick;

        private void Awake()
        {
            _timeTick = _defaultTimeTick;
            _sleepTime = new WaitForSeconds(_defaultTimeTick);
        }

        private void Start()
        {
            StartCoroutine(ActionAi());
        }

        [Inject]
        public void Construct(IBrain brain)
        {
            _brain = brain ?? throw new System.ArgumentNullException(nameof(brain));
        }

        private IEnumerator ActionAi()
        {
            yield return new WaitForSeconds(_timeBeforeStart);

            _brain.Init();

            while (gameObject.activeSelf)
            {
#if UNITY_EDITOR
                if (DebugSetting.IsAiDisbale) //todo remove on release
                {
                    yield return _sleepTime;

                    continue;
                }
#endif
                float timeElapsed = 0;

                while (_brain.ThinkCompleted == false)
                {
                    _brain.ThinkDuringTick();

                    yield return null;

                    timeElapsed += Time.deltaTime;
                }

                if (_timeTick != _defaultTimeTick - timeElapsed)
                {
                    _timeTick = _defaultTimeTick - timeElapsed;

                    if (_timeTick <= 0)
                        _timeTick = _defaultTimeTick;

                    _sleepTime = new WaitForSeconds(_timeTick);
                }

                if (_brain.TryGetCommand(out ICommand command))
                    command.Execute();

                yield return _sleepTime;
            }
        }
    }
}