using UnityEngine;

using ZombieVsMatch3.UI;

namespace ZombieVsMatch3.Gameplay.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Static data/Level")]
    public class LevelStaticData : ScriptableObject
    {
        public HudView hudViewPrefab;

        public Match3StaticData match3Data;
    }
}