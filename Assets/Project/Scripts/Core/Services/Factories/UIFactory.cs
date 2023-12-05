using ZombieVsMatch3.Core.Services.Assets;
using ZombieVsMatch3.UI;

namespace ZombieVsMatch3.Core.Services.Factories
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
    }
}