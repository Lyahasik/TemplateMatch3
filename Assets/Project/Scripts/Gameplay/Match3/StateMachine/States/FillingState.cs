using UnityEngine;

using ZombieVsMatch3.Core.Services.GameStateMachine.States;
using ZombieVsMatch3.Gameplay.Match3.Services;

namespace ZombieVsMatch3.Gameplay.Match3.StateMachine.States
{
    public class FillingState : IState
    {
        private readonly IFillingCellsMatch3Service _fillingCellsMatch3Service;

        public FillingState(IFillingCellsMatch3Service fillingCellsMatch3Service)
        {
            _fillingCellsMatch3Service = fillingCellsMatch3Service;
        }

        public void Enter()
        {
            Debug.Log($"Start state { GetType().Name }");
            
            _fillingCellsMatch3Service.DistributeStones();
        }

        public void Exit() {}
    }
}