using UnityEngine;
using UnityEngine.UI;

using ZombieVsMatch3.Core.Services.Scene;
using ZombieVsMatch3.Gameplay.Match3.Services;
using ZombieVsMatch3.Gameplay.StaticData;
using ZombieVsMatch3.UI.Hud.ResourcesGameplay;

namespace ZombieVsMatch3.UI.Hud
{
    public class HudView : MonoBehaviour
    {
        [SerializeField] private ResourcesView resourcesView;
        [SerializeField] private Button buttonMenu;

        private ISceneProviderService _sceneProviderService;

        public void Construct(ISceneProviderService sceneProviderService)
        {
            _sceneProviderService = sceneProviderService;
        }

        public void Initialize(IStoneCounterService stoneCounterService,
            Match3StaticData match3StaticData)
        {
            buttonMenu.onClick.AddListener(LoadLevel);
            resourcesView.Initialize(stoneCounterService, match3StaticData.stones);
        }
        
        private void LoadLevel()
        {
            _sceneProviderService.LoadMainScene();
        }
    }
}
