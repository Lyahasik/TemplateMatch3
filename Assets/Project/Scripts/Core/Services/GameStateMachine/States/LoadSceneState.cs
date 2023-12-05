using UnityEngine;

using ZombieVsMatch3.Core.Coroutines;
using ZombieVsMatch3.Core.Services.Scene;
using ZombieVsMatch3.UI;

namespace ZombieVsMatch3.Core.Services.GameStateMachine.States
{
    public class LoadSceneState : IState
    {
        private const string SceneName = "MainMenu";
        
        private readonly ISceneProviderService _sceneProviderService;
        private readonly CoroutinesContainer _coroutinesContainer;
        private readonly LoadingCurtain _curtain;

        public LoadSceneState(ISceneProviderService sceneProviderService,
            CoroutinesContainer coroutinesContainer,
            LoadingCurtain curtain)
        {
            _sceneProviderService = sceneProviderService;
            _coroutinesContainer = coroutinesContainer;
            _curtain = curtain;
        }

        public void Enter()
        {
            Debug.Log($"Start state { GetType().Name }");
            
            _sceneProviderService.LoadMainScene(SceneName);
        }

        public void Exit()
        {
            _curtain.Hide(_coroutinesContainer);
        }
    }
}