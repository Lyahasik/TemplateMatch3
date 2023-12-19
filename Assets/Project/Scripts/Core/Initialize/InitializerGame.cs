using Unity.VisualScripting;
using UnityEngine;

using ZombieVsMatch3.Core.Services;
using ZombieVsMatch3.Core.Services.Assets;
using ZombieVsMatch3.Core.Services.Factories.Gameplay;
using ZombieVsMatch3.Core.Services.Factories.UI;
using ZombieVsMatch3.Core.Services.GameStateMachine;
using ZombieVsMatch3.Core.Services.GameStateMachine.States;
using ZombieVsMatch3.Core.Services.Progress;
using ZombieVsMatch3.Core.Services.Scene;
using ZombieVsMatch3.UI;

namespace ZombieVsMatch3.Core.Initialize
{
    public class InitializerGame : MonoBehaviour
    {
        [SerializeField] private LoadingCurtain curtainPrefab;

        private ServicesContainer _servicesContainer;
        
        private void Awake()
        {
            LoadingCurtain curtain = Instantiate(curtainPrefab);
            curtain.name = curtainPrefab.name;

            _servicesContainer = new ServicesContainer();

            GameData gameData = GameDataCreate(curtain, _servicesContainer);
            
            RegisterServices(gameData);
            _servicesContainer.Single<IGameStateMachine>().Enter<LoadProgressState>();
            
            DontDestroyOnLoad(gameData);
        }

        private void RegisterServices(GameData gameData)
        {
            GameStateMachine gameStateMachine = new GameStateMachine();
            
            _servicesContainer.Register<IProgressProviderService>(new ProgressProviderService(gameStateMachine));
            _servicesContainer.Register<IRealtimeProgressService>(new RealtimeProgressService());
            
            _servicesContainer.Register<IAssetProvider>(new AssetProvider());
            _servicesContainer.Register<IUIFactory>(new UIFactory(
                _servicesContainer.Single<IAssetProvider>()));
            _servicesContainer.Register<IGameplayFactory>(new GameplayFactory(
                _servicesContainer.Single<IAssetProvider>()));

            _servicesContainer.Register<ISceneProviderService>(new SceneProviderService(
                gameStateMachine,
                _servicesContainer.Single<IUIFactory>(),
                _servicesContainer.Single<IGameplayFactory>()));

            gameStateMachine.Initialize(
                _servicesContainer.Single<IProgressProviderService>(),
                gameData.CoroutinesContainer,
                gameData.Curtain);
            _servicesContainer.Single<ISceneProviderService>().LoadMainScene();
            _servicesContainer.Register<IGameStateMachine>(gameStateMachine);
        }

        private GameData GameDataCreate(LoadingCurtain curtain, ServicesContainer servicesContainer)
        {
            GameData gameData = new GameObject().AddComponent<GameData>();
            gameData.name = nameof(GameData);
            
            Coroutines.CoroutinesContainer coroutinesContainer = gameData.AddComponent<Coroutines.CoroutinesContainer>();
            gameData.Construct(curtain, coroutinesContainer, servicesContainer);
            
            return gameData;
        }
    }
}
