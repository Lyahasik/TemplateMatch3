using UnityEngine;

using ZombieVsMatch3.UI;

namespace ZombieVsMatch3.Core.Services.GameStateMachine.States
{
    public class InitializeGameState : IState
    {
        private readonly LoadingCurtain _curtain;

        public InitializeGameState(LoadingCurtain curtain)
        {
            _curtain = curtain;
        }

        public void Enter()
        {
            Debug.Log($"Start state { GetType() }");
        }

        public void Exit() {}
    }
}