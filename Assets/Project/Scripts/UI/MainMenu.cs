using UnityEngine;
using UnityEngine.UI;

using ZombieVsMatch3.Core.Services.Scene;

namespace ZombieVsMatch3.UI
{
    public class MainMenu : MonoBehaviour
    {
        private const string SceneName = "Level1";

        private ISceneProviderService _sceneProviderService;
        
        [SerializeField] private Button buttonStartGame;

        public void Construct(ISceneProviderService sceneProviderService)
        {
            _sceneProviderService = sceneProviderService;
        }

        public void Initialize()
        {
            buttonStartGame.onClick.AddListener(LoadLevel);
        }

        private void LoadLevel()
        {
            _sceneProviderService.LoadLevel(SceneName);
        }
    }
}