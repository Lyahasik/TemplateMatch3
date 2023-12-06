using System;
using UnityEngine;
using UnityEngine.SceneManagement;

using ZombieVsMatch3.Core.Services.Factories;
using ZombieVsMatch3.Core.Services.GameStateMachine;
using ZombieVsMatch3.Core.Services.GameStateMachine.States;
using ZombieVsMatch3.UI;

namespace ZombieVsMatch3.Core.Services.Scene
{
    public class SceneProviderService : ISceneProviderService
    {
        private const string SceneMainMenuName = "MainMenu";
        
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IUIFactory _uiFactory;

        private string _nameNewActiveScene;

        private bool _isMainMenuInit;

        public SceneProviderService(IGameStateMachine gameStateMachine, IUIFactory uiFactory)
        {
            _gameStateMachine = gameStateMachine;
            _uiFactory = uiFactory;
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

            Hud hud = _uiFactory.CreateHUD();
            hud.Construct(this);
            hud.Initialize();
            
            Debug.Log("Level scene loaded.");
            _gameStateMachine.Enter<GameplayState>();
        }
    }
}