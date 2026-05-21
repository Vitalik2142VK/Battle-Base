using System.Collections.Generic;
using BattleBase.Gameplay.Actors;
using UnityEngine;

namespace BattleBase.Gameplay
{
    public interface IEntityFactory
    {
        public void SetBarracksInfos(IReadOnlyList<IActorData> barracksItemInfos);

        public void SetMachineFactoryInfos(IReadOnlyList<IActorData> machineFactoryItemInfos);

        public ITrackable Create(Trackable prefab, Transform target);
    }
}