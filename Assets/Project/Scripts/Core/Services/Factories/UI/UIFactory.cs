using ZombieVsMatch3.Core.Services.Assets;
using ZombieVsMatch3.UI;

namespace ZombieVsMatch3.Core.Services.Factories.UI
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;

        public UIFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public MainMenu CreateMainMenu()
        {
            MainMenu mainMenu = _assetProvider.Instantiate(AssetPath.UIMainMenu).GetComponent<MainMenu>();

            return mainMenu;
        }

        public Hud CreateHUD()
        {
            Hud hud = _assetProvider.Instantiate(AssetPath.UIHUD).GetComponent<Hud>();

            return hud;
        }
    }
}