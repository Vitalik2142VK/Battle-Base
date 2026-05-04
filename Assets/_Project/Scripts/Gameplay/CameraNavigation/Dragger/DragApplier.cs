using UnityEngine;

namespace BattleBase.Gameplay.CameraNavigation
{
    public class DragApplier
    {
        private readonly Transform _cameraRig;
        private readonly ResistanceCalculator _resistance;
        private readonly PositionRestrictor _restrictor;

        public DragApplier(Transform rig, ResistanceCalculator resistance, PositionRestrictor restrictor)
        {
            _cameraRig = rig;
            _resistance = resistance;
            _restrictor = restrictor;
        }

        public void Apply(Vector3 worldDelta)
        {            
            Vector3 deltaGround = _cameraRig.right * worldDelta.x + _cameraRig.forward * worldDelta.z;
            deltaGround.y = 0;
            Vector3 desiredPos = _cameraRig.position - deltaGround;
            Vector3 correctedDelta = _resistance.Calculate(deltaGround, desiredPos);
            Vector3 finalPos = _cameraRig.position - correctedDelta;
            _cameraRig.position = _restrictor.Restrict(finalPos, _cameraRig.position);
        }
    }
}