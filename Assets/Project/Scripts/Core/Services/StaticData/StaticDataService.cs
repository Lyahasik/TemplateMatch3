using UnityEngine;

using ZombieVsMatch3.Constants;
using ZombieVsMatch3.Gameplay.StaticData;
using ZombieVsMatch3.MainMenu.StaticData;
using ZombieVsMatch3.UI.StaticData;

namespace ZombieVsMatch3.Core.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private UIStaticData _ui; 
            
        private MainMenuStaticData _mainMenu;
        
        private LevelStaticData _level;
        
        public void Load()
        {
            _ui = Resources
                .Load<UIStaticData>(ConstantPaths.UI_DATA_PATH);
            
            _mainMenu = Resources
                .Load<MainMenuStaticData>(ConstantPaths.MAIN_MENU_DATA_PATH);
            
            _level = Resources
                .Load<LevelStaticData>(ConstantPaths.LEVEL_DATA_PATH);
        }

        public UIStaticData ForUI() => 
            _ui;

        public MainMenuStaticData ForMainMenu() => 
            _mainMenu;

        public LevelStaticData ForLevel() => 
            _level;
    }
}