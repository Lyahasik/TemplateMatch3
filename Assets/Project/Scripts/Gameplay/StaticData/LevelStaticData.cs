using UnityEngine;
using UnityEngine.Serialization;

using ZombieVsMatch3.UI;

namespace ZombieVsMatch3.Gameplay.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Static data/Level")]
    public class LevelStaticData : ScriptableObject
    {
        [FormerlySerializedAs("hudPrefab")] public HudView hudViewPrefab;

        public Match3StaticData match3Data;
    }
}