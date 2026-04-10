using DG.Tweening;
using UnityEngine;

namespace BattleBase.UI.PopUps
{
    public abstract class PopUpAnimatorBase : MonoBehaviour
    {
        public abstract void Init();

        public abstract bool TryPlayShow(out Tweener tweener);

        public abstract bool TryPlayHide(out Tweener tweener);
    }
}