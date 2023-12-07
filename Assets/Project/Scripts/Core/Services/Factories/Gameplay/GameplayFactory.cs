using ZombieVsMatch3.Core.Services.Assets;
using ZombieVsMatch3.Gameplay.Match3;

namespace ZombieVsMatch3.Core.Services.Factories.Gameplay
{
    public class GameplayFactory : IGameplayFactory
    {
        private readonly IAssetProvider _assetProvider;

        public GameplayFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public FieldMatch3 CreateMatch3()
        {
            FieldMatch3 field = _assetProvider.Instantiate(AssetPath.GameplayFieldMatch3).GetComponent<FieldMatch3>();

            return field;
        }
    }
}