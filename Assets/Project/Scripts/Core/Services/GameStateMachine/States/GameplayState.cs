using UnityEngine;

namespace ZombieVsMatch3.Core.Services.GameStateMachine.States
{
    public class GameplayState : IState
    {
        public GameplayState()
        {
            
        }

        public void Enter()
        {
            Debug.Log($"Start state { GetType().Name }");
        }

        public void Exit()
        {
            
        }
    }
}