using UnityEngine;

using ZombieVsMatch3.Core.Services.GameStateMachine.States;
using ZombieVsMatch3.Gameplay.Match3.Services;

namespace ZombieVsMatch3.Gameplay.Match3.StateMachine.States
{
    public class DestructionState : IState
    {
        private readonly IStonesDestructionMatch3Service _stonesDestructionMatch3Service;
        
        public DestructionState(IStonesDestructionMatch3Service stonesDestructionMatch3Service)
        {
            _stonesDestructionMatch3Service = stonesDestructionMatch3Service;
        }

        public void Enter()
        {
            Debug.Log($"Start state { GetType().Name }");
            
            _stonesDestructionMatch3Service.DestroyForms();
        }

        public void Exit() {}
    }
}