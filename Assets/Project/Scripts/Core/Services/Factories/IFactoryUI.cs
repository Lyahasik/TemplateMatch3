using ZombieVsMatch3.UI;

namespace ZombieVsMatch3.Core.Services.Factories
{
    public interface IUIFactory : IService
    {
        public MainMenu CreateMainMenu();
    }
}