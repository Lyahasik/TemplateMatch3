using ZombieVsMatch3.Gameplay.StaticData;
using ZombieVsMatch3.MainMenu.StaticData;
using ZombieVsMatch3.UI.StaticData;

namespace ZombieVsMatch3.Core.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        public UIStaticData ForUI();
        public MainMenuStaticData ForMainMenu();
        public LevelStaticData ForLevel();
    }
}