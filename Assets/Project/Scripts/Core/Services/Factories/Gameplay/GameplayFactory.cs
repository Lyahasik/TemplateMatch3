using UnityEngine;

using ZombieVsMatch3.Core.Services.StaticData;
using ZombieVsMatch3.Gameplay.Match3;
using ZombieVsMatch3.Gameplay.StaticData;

namespace ZombieVsMatch3.Core.Services.Factories.Gameplay
{
    public class GameplayFactory : IGameplayFactory
    {
        private readonly IStaticDataService _staticDataService;

        public GameplayFactory(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public FieldMatch3 CreateMatch3()
        {
            LevelStaticData levelData = _staticDataService.ForLevel();
            FieldMatch3 field = Object.Instantiate(levelData.match3Data.fieldPrefab);

            return field;
        }
    }
}