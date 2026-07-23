using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Economy
{
    [CreateAssetMenu(
    fileName = nameof(MaterialCreatorSource),
    menuName = AssetMenuPaths.ScriptableObjects + nameof(ActorConfig) + "/" + nameof(MaterialCreatorSource))]
    public class MaterialCreatorSource : ActorComponentSource, IMaterialCreatorSource
    {
        [SerializeField] private MaterialCreatorConfig _config;

        public IMaterialCreatorConfig Config => _config;
    }
}