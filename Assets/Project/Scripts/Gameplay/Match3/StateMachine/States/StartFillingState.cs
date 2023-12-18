using UnityEngine;

using ZombieVsMatch3.Core.Services.GameStateMachine.States;
using ZombieVsMatch3.Gameplay.Match3.Services;

namespace ZombieVsMatch3.Gameplay.Match3.StateMachine.States
{
    public class StartFillingState : IState
    {
        private readonly IFillingCellsMatch3Service _fillingCellsMatch3Service;
        private readonly FieldMatch3 _fieldMatch3;

        public StartFillingState(IFillingCellsMatch3Service fillingCellsMatch3Service,
            FieldMatch3 fieldMatch3)
        {
            _fillingCellsMatch3Service = fillingCellsMatch3Service;
            _fieldMatch3 = fieldMatch3;
        }

        public void Enter()
        {
            Debug.Log($"Start state { GetType().Name }");
            
            _fillingCellsMatch3Service.Initialize();
            _fillingCellsMatch3Service.FirstFilling(_fieldMatch3);
        }

        public void Exit() {}
    }
}