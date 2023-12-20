using UnityEngine;
using UnityEngine.UI;

using ZombieVsMatch3.Constants;
using ZombieVsMatch3.Core.Services.Scene;

namespace ZombieVsMatch3.UI
{
    public class MainMenuView : MonoBehaviour
    {
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
            _sceneProviderService.LoadLevel(ConstantValues.SCENE_NAME_LEVEL);
        }
    }
}