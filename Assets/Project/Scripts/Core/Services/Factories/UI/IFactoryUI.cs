using ZombieVsMatch3.UI.Hud;
using ZombieVsMatch3.UI.MainMenu;

namespace ZombieVsMatch3.Core.Services.Factories.UI
{
    public interface IUIFactory : IService
    {
        public MainMenuView CreateMainMenu();
        public HudView CreateHUD();
    }
}