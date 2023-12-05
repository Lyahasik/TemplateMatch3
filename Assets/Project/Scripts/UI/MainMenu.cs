using UnityEngine;

using ZombieVsMatch3.Core.Services.Scene;

namespace ZombieVsMatch3.UI
{
    public class MainMenu : MonoBehaviour
    {
        private ISceneProviderService _sceneProviderService;
        
        public void Construct(ISceneProviderService sceneProviderService)
        {
            _sceneProviderService = sceneProviderService;
        }

        public void Initialize()
        {
            
        }
    }
}