using UnityEngine;
using UnityEngine.UI;

using ZombieVsMatch3.Core.Services.Scene;

namespace ZombieVsMatch3.UI
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private Button buttonMenu;

        private ISceneProviderService _sceneProviderService;

        public void Construct(ISceneProviderService sceneProviderService)
        {
            _sceneProviderService = sceneProviderService;
        }

        public void Initialize()
        {
            buttonMenu.onClick.AddListener(LoadLevel);
        }
        
        private void LoadLevel()
        {
            _sceneProviderService.LoadMainScene();
        }
    }
}
