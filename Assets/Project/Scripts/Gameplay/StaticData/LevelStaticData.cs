using UnityEngine;

using ZombieVsMatch3.UI.Hud;

namespace ZombieVsMatch3.Gameplay.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Static data/Level")]
    public class LevelStaticData : ScriptableObject
    {
        public HudView hudViewPrefab;

        public Match3StaticData match3Data;
    }
}