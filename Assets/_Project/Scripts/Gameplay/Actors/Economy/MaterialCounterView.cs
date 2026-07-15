using BattleBase.UI.PopUps;
using System.Collections;
using TMPro;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Economy
{
    public class MaterialCounterView : MonoBehaviour, IMaterialCreatorView
    {
        private const string Format = "+{0}";

        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private PopUp _popUp;
        [SerializeField] private TMP_Text _counter;
        [SerializeField][Range(0.1f, 3f)] private float _timeShowing = 1f;

        private IMaterialCreatorEvents _events;
        private WaitForSeconds _hideWaiting;
        private Coroutine _coroutine;

        private void Awake()
        {
            _hideWaiting = new WaitForSeconds(_timeShowing);
        }

        private void OnEnable()
        {
            if (_events != null)
                _events.MaterialsCreated += OnUpdateData;

            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.ignoreParentGroups = false;
        }

        private void OnDisable()
        {
            if (_events != null)
                _events.MaterialsCreated -= OnUpdateData;
        }

        public void Init(IMaterialCreatorEvents events)
        {
            _events = events ?? throw new System.ArgumentNullException(nameof(events));
            _popUp.Init();

            if (gameObject.activeSelf)
                _events.MaterialsCreated += OnUpdateData;
        }

        private IEnumerator WaitHide()
        {
            yield return _hideWaiting;

            _popUp.Hide();
            _coroutine = null;
        }

        private void OnUpdateData(int materials)
        {
            _counter.text = string.Format(Format, materials);
            _popUp.Show();

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(WaitHide());
        }
    }
}