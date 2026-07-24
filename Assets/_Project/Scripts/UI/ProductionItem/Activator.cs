using UnityEngine;

namespace BattleBase.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Activator : MonoBehaviour
    {
        [SerializeField] private CanvasGroupData _disableData;

        private CanvasGroup _canvasGroup;
        private CanvasGroupData _enableData;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _enableData = new CanvasGroupData(
                _canvasGroup.alpha,
                _canvasGroup.interactable,
                _canvasGroup.blocksRaycasts,
                _canvasGroup.ignoreParentGroups);
        }

        public void SetActive(bool isActive)
        {
            if (isActive)
                SetCanvasGroupData(_enableData);
            else
                SetCanvasGroupData(_disableData);
        }

        private void SetCanvasGroupData(CanvasGroupData data)
        {
            _canvasGroup.alpha = data.Alpha;
            _canvasGroup.interactable = data.IsInteractable;
            _canvasGroup.blocksRaycasts = data.IsBlocksRaycasts;
            _canvasGroup.ignoreParentGroups = data.IsIgnoreParentGroups;
        }

        [System.Serializable]
        private struct CanvasGroupData
        {
            [SerializeField] private float _alpha;
            [SerializeField] private bool _isInteractable;
            [SerializeField] private bool _isBlocksRaycasts;
            [SerializeField] private bool _isIgnoreParentGroups;

            public CanvasGroupData(float alpha, bool isInteractable, bool isBlocksRaycasts, bool isIgnoreParentGroups)
            {
                _alpha = alpha;
                _isInteractable = isInteractable;
                _isBlocksRaycasts = isBlocksRaycasts;
                _isIgnoreParentGroups = isIgnoreParentGroups;
            }

            public float Alpha => _alpha;

            public bool IsInteractable => _isInteractable;

            public bool IsBlocksRaycasts => _isBlocksRaycasts;

            public bool IsIgnoreParentGroups => _isIgnoreParentGroups;
        }
    }
}