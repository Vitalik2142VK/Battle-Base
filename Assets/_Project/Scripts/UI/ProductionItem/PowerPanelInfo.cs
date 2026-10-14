using TMPro;
using UnityEngine;

namespace BattleBase.UI
{
    public class PowerPanelInfo : MonoBehaviour
    {
        [SerializeField] private TMP_Text _powerCount;

        public bool IsInitialized { get; private set; } = false;

        public void Init(int power)
        {
            _powerCount.text = power.ToString();

            IsInitialized = true;
        }

        public void Show() => 
            gameObject.SetActive(true);

        public void Hide() => 
            gameObject.SetActive(false);
    }
}