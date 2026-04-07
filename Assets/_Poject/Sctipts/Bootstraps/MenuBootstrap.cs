using BattleBase.Services.InputReader;
using UnityEngine;
using VContainer;

namespace BattleBase.Bootstraps
{
    public class MenuBootstrap : MonoBehaviour
    {
        private IInputReader _inputReader;

        [Inject]
        public void Construct(IInputReader inputReader)
        {
            _inputReader = inputReader;
        }

        private void Start()
        {

        }
    }
}