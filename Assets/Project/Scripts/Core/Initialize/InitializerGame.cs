using Unity.VisualScripting;
using UnityEngine;

using ZombieVsMatch3.Core.Services;
using ZombieVsMatch3.Core.Services.Factories.Gameplay;
using ZombieVsMatch3.Core.Services.Factories.UI;
using ZombieVsMatch3.Core.Services.GameStateMachine;
using ZombieVsMatch3.Core.Services.GameStateMachine.States;
using ZombieVsMatch3.Core.Services.Progress;
using ZombieVsMatch3.Core.Services.Scene;
using ZombieVsMatch3.Core.Services.StaticData;
using ZombieVsMatch3.UI.Loading;

namespace ZombieVsMatch3.Core.Initialize
{
    public class InitializerGame : MonoBehaviour
    {
        [SerializeField] private LoadingCurtain curtainPrefab;

        private ServicesContainer _servicesContainer;
        
        private void Awake()
        {
            RegisterServices();
            _servicesContainer.Single<IGameStateMachine>().Enter<LoadProgressState>();
        }

        private void RegisterServices()
        {
            _servicesContainer = new ServicesContainer();

            RegisterStaticDataService();

            GameStateMachine gameStateMachine = new GameStateMachine();
            _servicesContainer.Register<IProgressProviderService>(new ProgressProviderService(gameStateMachine));
            _servicesContainer.Register<IRealtimeProgressService>(new RealtimeProgressService());
            
            _servicesContainer.Register<IUIFactory>(new UIFactory(
                _servicesContainer.Single<IStaticDataService>()));
            _servicesContainer.Register<IGameplayFactory>(new GameplayFactory(
                _servicesContainer.Single<IStaticDataService>()));

            _servicesContainer.Register<ISceneProviderService>(new SceneProviderService(
                gameStateMachine,
                _servicesContainer.Single<IUIFactory>(),
                _servicesContainer.Single<IGameplayFactory>(),
                _servicesContainer.Single<IStaticDataService>()));
            
            LoadingCurtain curtain = CreateLoadingCurtain();
            GameData gameData = GameDataCreate(curtain, _servicesContainer);

            gameStateMachine.Initialize(
                _servicesContainer.Single<IProgressProviderService>(),
                gameData.CoroutinesContainer,
                gameData.Curtain);
            _servicesContainer.Single<ISceneProviderService>().LoadMainScene();
            _servicesContainer.Register<IGameStateMachine>(gameStateMachine);
            
            DontDestroyOnLoad(gameData);
        }

        private LoadingCurtain CreateLoadingCurtain()
        {
            LoadingCurtain curtain = Instantiate(curtainPrefab);
            curtain.Construct(curtainPrefab.name,
                _servicesContainer.Single<IStaticDataService>().ForUI());
            
            return curtain;
        }

        private void RegisterStaticDataService()
        {
            StaticDataService staticDataService = new StaticDataService();
            staticDataService.Load();
            _servicesContainer.Register<IStaticDataService>(staticDataService);
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
