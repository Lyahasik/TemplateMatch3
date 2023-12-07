using ZombieVsMatch3.UI;

namespace ZombieVsMatch3.Core.Services.Factories.UI
{
    public interface IUIFactory : IService
    {
        public MainMenu CreateMainMenu();
        public Hud CreateHUD();
    }
}