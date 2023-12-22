using UnityEngine;

using ZombieVsMatch3.Core.Services.StaticData;
using ZombieVsMatch3.Gameplay.StaticData;
using ZombieVsMatch3.MainMenu.StaticData;
using ZombieVsMatch3.UI.Hud;
using ZombieVsMatch3.UI.MainMenu;

namespace ZombieVsMatch3.Core.Services.Factories.UI
{
    public class UIFactory : IUIFactory
    {
        private readonly IStaticDataService _staticDataService;

        public UIFactory(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public MainMenuView CreateMainMenu()
        {
            MainMenuStaticData mainMenuData = _staticDataService.ForMainMenu();
            MainMenuView mainMenu = Object.Instantiate(mainMenuData.mainMenuViewPrefab);

            return mainMenu;
        }

        public HudView CreateHUD()
        {
            LevelStaticData levelData = _staticDataService.ForLevel();
            HudView hudView = Object.Instantiate(levelData.hudViewPrefab);

            return hudView;
        }
    }
}