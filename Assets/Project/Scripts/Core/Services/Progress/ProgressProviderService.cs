using UnityEngine;

using ZombieVsMatch3.Core.Services.GameStateMachine;
using ZombieVsMatch3.Core.Services.GameStateMachine.States;

namespace ZombieVsMatch3.Core.Services.Progress
{
    public class ProgressProviderService : IProgressProviderService
    {
        private readonly IGameStateMachine _gameStateMachine;

        public ProgressProviderService(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public PlayerProgress LoadProgress()
        {
            Debug.Log("Loaded progress.");
            
            _gameStateMachine.Enter<LoadSceneState>();
            
            return new PlayerProgress();
        }

        public void SaveProgress()
        {
            
        }
    }
}