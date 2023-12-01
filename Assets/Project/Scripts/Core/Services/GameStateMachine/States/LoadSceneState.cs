using UnityEngine;

using ZombieVsMatch3.UI;

namespace ZombieVsMatch3.Core.Services.GameStateMachine.States
{
    public class LoadSceneState : IState
    {
        private readonly CoroutineContainer.CoroutineContainer _coroutineContainer;
        private readonly LoadingCurtain _curtain;

        public LoadSceneState(CoroutineContainer.CoroutineContainer coroutineContainer, LoadingCurtain curtain)
        {
            _coroutineContainer = coroutineContainer;
            _curtain = curtain;
        }

        public void Enter()
        {
            Debug.Log($"Start state { GetType() }");
            // _curtain.Show();
        }

        public void Exit()
        {
            // _curtain.Hide(_coroutineContainer);
        }
    }
}