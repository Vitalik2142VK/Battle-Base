using System;
using BattleBase.Commands;
using BattleBase.Core;
using BattleBase.UI.Buttons;
using BattleBase.UI.PopUps;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Map
{
    public class TerritorySelectPopUp : MonoBehaviour, IPoolable<TerritorySelectPopUp>
    {
        [SerializeField] private PopUp _popUp;
        [SerializeField] private ButtonClickHandler _battleButton;        

        private Transform _target;

        public event Action<TerritorySelectPopUp> Deactivated;

        [Inject]
        public void Construct(LoadGameSceneCommand loadGameSceneCommand)
        {
            if(loadGameSceneCommand == null)
                throw new ArgumentNullException(nameof(loadGameSceneCommand));

            _battleButton.AddCommand(loadGameSceneCommand);
        }

        private void Update()
        {
            if (_target != null)
                transform.position = _target.position;
        }

        public void Init()
        {
            _popUp.Init();
            _popUp.HideInstantly();           
            Update();
        }

        public void SetTarget(Transform target) =>
            _target = target != null ? target : throw new ArgumentNullException(nameof(target));

        public void Show()
        {
            gameObject.SetActive(true);
            _popUp.Show();
        }

        public void Hide() =>
            _popUp.Hide(Deactivate);

        public void HideBattleButton() =>
            _battleButton.Hide();

        public void Deactivate()
        {
            _battleButton.Show();

            Deactivated?.Invoke(this);
        }
    }
}
