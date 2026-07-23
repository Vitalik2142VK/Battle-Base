using UnityEngine;

namespace BattleBase.PreviewCreatingSystem
{
    public interface IModelCenterCalculator
    {
        public Vector3 GetCenter(GameObject model);
    }
}