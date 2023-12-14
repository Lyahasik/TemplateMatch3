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
using ZombieVsMatch3.Gameplay.Match3;
using ZombieVsMatch3.Gameplay.Match3.Services;
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
            GameStateMachine gameStateMachine = new GameStateMachine(gameData);
            
            gameData.ServicesContainer.Register<IProgressProviderService>(new ProgressProviderService(gameStateMachine));
            gameData.ServicesContainer.Register<IRealtimeProgressService>(new RealtimeProgressService());
            
            gameData.ServicesContainer.Register<IAssetProvider>(new AssetProvider());
            gameData.ServicesContainer.Register<IUIFactory>(new UIFactory(
                _servicesContainer.Single<IAssetProvider>()));
            gameData.ServicesContainer.Register<IGameplayFactory>(new GameplayFactory(
                _servicesContainer.Single<IAssetProvider>()));

            gameData.ServicesContainer.Register<IDefiningConnectionsMatch3Service>(
                new DefiningConnectionsMatch3Service());
            gameData.ServicesContainer.Register<ICellActivityCheckService>(new CellActivityCheckService());
            gameData.ServicesContainer.Register<IStonesDestructionMatch3Service>(new StonesDestructionMatch3Service(
                _servicesContainer.Single<ICellActivityCheckService>(),
                _servicesContainer.Single<IDefiningConnectionsMatch3Service>(),
                gameData.CoroutinesContainer));
            gameData.ServicesContainer.Register<IExchangeOfStonesService>(new ExchangeOfStonesService(
                _servicesContainer.Single<IDefiningConnectionsMatch3Service>(),
                _servicesContainer.Single<IStonesDestructionMatch3Service>()));
            gameData.ServicesContainer.Register<IFillingCellsMatch3Service>(new FillingCellsMatch3Service(
                _servicesContainer.Single<IDefiningConnectionsMatch3Service>(),
                _servicesContainer.Single<ICellActivityCheckService>(),
                _servicesContainer.Single<IStonesDestructionMatch3Service>()));

            gameData.ServicesContainer.Register<ISceneProviderService>(new SceneProviderService(
                gameStateMachine,
                _servicesContainer.Single<IUIFactory>(),
                _servicesContainer.Single<IGameplayFactory>(),
                _servicesContainer.Single<IFillingCellsMatch3Service>(),
                _servicesContainer.Single<ICellActivityCheckService>(),
                _servicesContainer.Single<IExchangeOfStonesService>()));

            gameStateMachine.Initialize();
            _servicesContainer.Single<ISceneProviderService>().LoadMainScene();
            gameData.ServicesContainer.Register<IGameStateMachine>(gameStateMachine);
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
