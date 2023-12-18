using UnityEngine;

using ZombieVsMatch3.Core.Services.GameStateMachine.States;
using ZombieVsMatch3.Gameplay.Match3.Services;

namespace ZombieVsMatch3.Gameplay.Match3.StateMachine.States
{
    public class StopState : IState
    {
        private readonly IFillingCellsMatch3Service _fillingCellsMatch3Service;
        private readonly FieldMatch3 _fieldMatch3;

        public void Enter()
        {
            Debug.Log($"Start state { GetType().Name }");
        }

        public void Exit() {}
    }
}