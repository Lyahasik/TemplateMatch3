using UnityEngine;

using ZombieVsMatch3.Core.Services.GameStateMachine.States;
using ZombieVsMatch3.Gameplay.Match3.Services;

namespace ZombieVsMatch3.Gameplay.Match3.StateMachine.States
{
    public class SwappingState : IState
    {
        private readonly IExchangeOfStonesService _exchangeOfStonesService;

        public SwappingState(IExchangeOfStonesService exchangeOfStonesService)
        {
            _exchangeOfStonesService = exchangeOfStonesService;
        }

        public void Enter()
        {
            Debug.Log($"Start state { GetType().Name }");

            _exchangeOfStonesService.StartSwap();
        }

        public void Exit() {}
    }
}