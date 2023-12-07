using System;
using UnityEngine;
using UnityEngine.SceneManagement;

using ZombieVsMatch3.Core.Services.Factories.Gameplay;
using ZombieVsMatch3.Core.Services.Factories.UI;
using ZombieVsMatch3.Core.Services.GameStateMachine;
using ZombieVsMatch3.Core.Services.GameStateMachine.States;
using ZombieVsMatch3.Gameplay.Match3;
using ZombieVsMatch3.Gameplay.Services;
using ZombieVsMatch3.UI;

namespace ZombieVsMatch3.Core.Services.Scene
{
    public class SceneProviderService : ISceneProviderService
    {
        private const string SceneMainMenuName = "MainMenu";
        
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IUIFactory _uiFactory;
        private readonly IGameplayFactory _gameplayFactory;
        private readonly IFillingCellsMatch3Service _fillingCellsMatch3Service;
        private readonly IDefiningConnectionsMatch3Service _definingConnectionsMatch3Service;

        private string _nameNewActiveScene;

        private bool _isMainMenuInit;

        public SceneProviderService(IGameStateMachine gameStateMachine,
            IUIFactory uiFactory,
            IGameplayFactory gameplayFactory,
            IFillingCellsMatch3Service fillingCellsMatch3Service,
            IDefiningConnectionsMatch3Service definingConnectionsMatch3Service)
        {
            _gameStateMachine = gameStateMachine;
            _uiFactory = uiFactory;
            _gameplayFactory = gameplayFactory;
            _fillingCellsMatch3Service = fillingCellsMatch3Service;
            _definingConnectionsMatch3Service = definingConnectionsMatch3Service;
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
            FieldMatch3 fieldMatch3 = _gameplayFactory.CreateMatch3();
            fieldMatch3.transform.SetParent(hud.transform, false);
            fieldMatch3.Initialize(_definingConnectionsMatch3Service);
            
            _fillingCellsMatch3Service.Initialize(fieldMatch3);
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