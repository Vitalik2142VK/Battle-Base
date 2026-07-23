using TMPro;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Energy
{
    public class PowerCounterView : MonoBehaviour, IPowerGeneratorView
    {
        [SerializeField] private TMP_Text _counter;

        private IPowerGeneratorNotifier _notifier;

        private void OnEnable()
        {
            if (_notifier != null)
            {
                _notifier.PowerChanged += OnUpdateData;

                OnUpdateData();
            }
        }

        private void OnDisable()
        {
            if (_notifier != null)
                _notifier.PowerChanged -= OnUpdateData;
        }

        public void Init(IPowerGeneratorNotifier notifier)
        {
            _notifier = notifier ?? throw new System.ArgumentNullException(nameof(notifier));

            if (gameObject.activeSelf)
            {
                _notifier.PowerChanged += OnUpdateData;

                OnUpdateData();
            }
        }

        private void OnUpdateData()
        {
            _counter.text = _notifier.PowerCount.ToString();
        }
    }
}
