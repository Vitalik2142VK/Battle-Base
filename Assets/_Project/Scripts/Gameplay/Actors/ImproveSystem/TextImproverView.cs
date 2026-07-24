using TMPro;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public class TextImproverView : MonoBehaviour, IImproverViewComponent
    {
        [SerializeField] private TMP_Text _text;

        private IImproverINotifier _improverNotifier;

        private void OnEnable()
        {
            if (_improverNotifier != null)
            {
                _improverNotifier.Improved += OnUpdateData;

                OnUpdateData();
            }
        }

        private void OnDisable()
        {
            if (_improverNotifier != null)
                _improverNotifier.Improved -= OnUpdateData;
        }

        public void Init(IImproverINotifier improverNotifier)
        {
            _improverNotifier = improverNotifier ?? throw new System.ArgumentNullException(nameof(improverNotifier));

            if (gameObject.activeSelf)
            {
                _improverNotifier.Improved += OnUpdateData;

                OnUpdateData();
            }
        }

        private void OnUpdateData()
        {
            _text.text = _improverNotifier.CurrentTier.ToString();
        }
    }
}