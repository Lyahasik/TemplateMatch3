using System;
using UnityEngine;
using UnityEngine.SceneManagement;

using ZombieVsMatch3.Core.Services.Factories.Gameplay;
using ZombieVsMatch3.Core.Services.Factories.UI;
using ZombieVsMatch3.Core.Services.GameStateMachine;
using ZombieVsMatch3.Core.Services.GameStateMachine.States;
using ZombieVsMatch3.Gameplay.Match3;
using ZombieVsMatch3.Gameplay.Match3.StateMachine;
using ZombieVsMatch3.Gameplay.Match3.Services;
using ZombieVsMatch3.Gameplay.Match3.StateMachine.States;
using ZombieVsMatch3.UI;

namespace ZombieVsMatch3.Core.Services.Scene
{
    public class SceneProviderService : ISceneProviderService
    {
        private const string SceneMainMenuName = "MainMenu";

        private readonly IGameStateMachine _gameStateMachine;
        private readonly ServicesContainer _servicesContainer;
        private readonly IUIFactory _uiFactory;
        private readonly IGameplayFactory _gameplayFactory;

        private Match3StateMachine _match3StateMachine;
        
        private string _nameNewActiveScene;

        private bool _isMainMenuInit;

        public SceneProviderService(IGameStateMachine gameStateMachine,
            ServicesContainer servicesContainer,
            IUIFactory uiFactory,
            IGameplayFactory gameplayFactory)
        {
            _gameStateMachine = gameStateMachine;
            _servicesContainer = servicesContainer;
            _uiFactory = uiFactory;
            _gameplayFactory = gameplayFactory;
        }

        public void LoadMainScene()
        {
            Debug.Log("Current active scene : " + SceneManager.GetActiveScene().name);

            if (_isMainMenuInit)
            {
                _gameStateMachine.Enter<LoadSceneState>();
                
                PrepareMainMenu();
                return;
            }

            LoadScene(SceneMainMenuName, PrepareMainMenuScene);
        }

        public void LoadLevel(string sceneName)
        {
            Debug.Log("Current active scene : " + SceneManager.GetActiveScene().name);
            _gameStateMachine.Enter<LoadSceneState>();
            
            LoadScene(sceneName, PrepareLevelScene);
        }

        private void LoadScene(string sceneName, Action<AsyncOperation> prepareScene)
        {
            _nameNewActiveScene = sceneName;
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive).completed += prepareScene;
        }

        private void PrepareMainMenuScene(AsyncOperation obj)
        {
            PrepareMainMenu();
            
            _isMainMenuInit = true;
        }

        private void PrepareMainMenu()
        {
            string oldSceneName = SceneManager.GetActiveScene().name;
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(_nameNewActiveScene));
            SceneManager.UnloadSceneAsync(oldSceneName);
            Debug.Log("New active scene : " + SceneManager.GetActiveScene().name);

            MainMenu mainMenu = _uiFactory.CreateMainMenu();
            mainMenu.Construct(this);
            mainMenu.Initialize();
            
            Debug.Log("Main scene loaded.");
            _gameStateMachine.Enter<MainMenuState>();
        }

        private void PrepareLevelScene(AsyncOperation obj)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(_nameNewActiveScene));
            Debug.Log("New active scene : " + SceneManager.GetActiveScene().name);

            Hud hud = CreateHUD();
            CreateMatch3(hud);

            Debug.Log("Level scene loaded.");
            _gameStateMachine.Enter<GameplayState>();
        }

        private void CreateMatch3(Hud hud)
        {
            RegisterServices();
            hud.Match3StateMachine = _match3StateMachine;

            FieldMatch3 fieldMatch3 = _gameplayFactory.CreateMatch3();
            fieldMatch3.transform.SetParent(hud.transform, false);
            fieldMatch3.Initialize(
                _servicesContainer.Single<IExchangeOfStonesService>(),
                _servicesContainer.Single<ICellsStateCheckService>());
            
            _match3StateMachine.Initialize(
                _servicesContainer.Single<IFillingCellsMatch3Service>(),
                fieldMatch3,
                fieldMatch3.FieldMatch3ActiveArea,
                _servicesContainer.Single<IExchangeOfStonesService>(),
                _servicesContainer.Single<IStonesDestructionMatch3Service>());
            _match3StateMachine.Enter<StartFillingState>();
        }

        //TODO implement cleaning
        private void RegisterServices()
        {
            if (_match3StateMachine != null)
                return;
            
            _match3StateMachine = new Match3StateMachine();

            _servicesContainer.Register<IDefiningConnectionsMatch3Service>(
                new DefiningConnectionsMatch3Service());
            _servicesContainer.Register<ICellsStateCheckService>(new CellsStateCheckService());
            _servicesContainer.Register<IStonesDestructionMatch3Service>(new StonesDestructionMatch3Service(
                _match3StateMachine,
                _servicesContainer.Single<ICellsStateCheckService>(),
                _servicesContainer.Single<IDefiningConnectionsMatch3Service>()));
            _servicesContainer.Register<IExchangeOfStonesService>(new ExchangeOfStonesService(
                _match3StateMachine,
                _servicesContainer.Single<IDefiningConnectionsMatch3Service>(),
                _servicesContainer.Single<ICellsStateCheckService>()));
            _servicesContainer.Register<IFillingCellsMatch3Service>(new FillingCellsMatch3Service(
                _match3StateMachine,
                _servicesContainer.Single<IDefiningConnectionsMatch3Service>(),
                _servicesContainer.Single<ICellsStateCheckService>()));
        }

        private Hud CreateHUD()
        {
            Hud hud = _uiFactory.CreateHUD();
            hud.Construct(this);
            hud.Initialize();

            return hud;
        }
    }
}