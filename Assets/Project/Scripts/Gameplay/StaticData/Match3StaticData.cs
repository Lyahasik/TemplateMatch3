using System;
using System.Collections.Generic;
using UnityEngine;

using ZombieVsMatch3.Gameplay.Match3;
using ZombieVsMatch3.Gameplay.Match3.Stones;

namespace ZombieVsMatch3.Gameplay.StaticData
{
    [Serializable]
    public class Match3StaticData
    {
        public FieldMatch3 fieldPrefab;
        
        public float speedStoneMovement;

        [Space]
        public List<StoneData> stones;
    }
}