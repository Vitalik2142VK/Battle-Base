using BattleBase.Utils.Constants;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors
{
    [CreateAssetMenu(
    fileName = nameof(AllActorConfigs),
    menuName = AssetMenuPaths.ScriptableObjects + nameof(AllActorConfigs))]
    public class AllActorConfigs : ScriptableObject, IAllActorConfigs
    {
        [SerializeField] private ActorConfig[] _configs;

        public IEnumerable<IActorConfig> Configs => _configs;
    }
}