using Unity.VisualScripting;
using UnityEngine;

using ZombieVsMatch3.Core.Services;
using ZombieVsMatch3.Core.Services.GameStateMachine;
using ZombieVsMatch3.Core.Services.GameStateMachine.States;
using ZombieVsMatch3.UI;

namespace ZombieVsMatch3.Core.Initialize
{
    public class InitializerGame : MonoBehaviour
    {
        [SerializeField] private LoadingCurtain curtainPrefab;
        
        private void Awake()
        {
            LoadingCurtain curtain = Instantiate(curtainPrefab);
            curtain.name = curtainPrefab.name;

            ServicesContainer servicesContainer = new ServicesContainer();

            GameData gameData = GameDataCreate(curtain, servicesContainer);
            
            RegisterServices(gameData);
            
            DontDestroyOnLoad(gameData);
        }

        private void RegisterServices(GameData gameData)
        {
            GameStateMachine gameStateMachine = GameStateMachineCreate(gameData);
            
            gameData.ServicesContainer.Register(gameStateMachine);
            
            gameStateMachine.Enter<LoadProgressState>();
        }

        private GameData GameDataCreate(LoadingCurtain curtain, ServicesContainer servicesContainer)
        {
            GameData gameData = new GameObject().AddComponent<GameData>();
            gameData.name = nameof(GameData);
            
            CoroutineContainer.CoroutineContainer coroutineContainer = gameData.AddComponent<CoroutineContainer.CoroutineContainer>();
            gameData.Construct(curtain, coroutineContainer, servicesContainer);
            
            return gameData;
        }

        private GameStateMachine GameStateMachineCreate(GameData gameData)
        {
            GameStateMachine gameStateMachine = new GameStateMachine(gameData);
            gameStateMachine.Instantiate();
            gameStateMachine.Enter<InitializeGameState>();

            return gameStateMachine;
        }
    }
}
