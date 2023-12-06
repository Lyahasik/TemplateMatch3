using UnityEngine;

using ZombieVsMatch3.Core.Coroutines;
using ZombieVsMatch3.UI;

namespace ZombieVsMatch3.Core.Services.GameStateMachine.States
{
    public class LoadSceneState : IState
    {
        private readonly CoroutinesContainer _coroutinesContainer;
        private readonly LoadingCurtain _curtain;

        public LoadSceneState(CoroutinesContainer coroutinesContainer,
            LoadingCurtain curtain)
        {
            _coroutinesContainer = coroutinesContainer;
            _curtain = curtain;
        }

        public void Enter()
        {
            Debug.Log($"Start state { GetType().Name }");
            
            _curtain.Show();
        }

        public void Exit()
        {
            _curtain.Hide(_coroutinesContainer);
        }
    }
}