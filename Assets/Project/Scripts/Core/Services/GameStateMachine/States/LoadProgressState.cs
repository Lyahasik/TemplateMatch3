using UnityEngine;

using ZombieVsMatch3.UI;

namespace ZombieVsMatch3.Core.Services.GameStateMachine.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly CoroutineContainer.CoroutineContainer _coroutineContainer;
        private readonly LoadingCurtain _curtain;

        public LoadProgressState(GameStateMachine gameStateMachine, CoroutineContainer.CoroutineContainer coroutineContainer, LoadingCurtain curtain)
        {
            _gameStateMachine = gameStateMachine;
            _coroutineContainer = coroutineContainer;
            _curtain = curtain;
        }

        public void Enter()
        {
            Debug.Log($"Start state { GetType() }");
            _gameStateMachine.Enter<LoadSceneState>();
        }

        public void Exit()
        {
            _curtain.Hide(_coroutineContainer);
        }
    }
}