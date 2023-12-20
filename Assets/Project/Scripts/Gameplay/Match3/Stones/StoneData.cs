using System;
using UnityEngine;

namespace ZombieVsMatch3.Gameplay.Match3.Stones
{
    [Serializable]
    public struct StoneData
    {
        public static StoneData Empty = new (StoneType.Empty, null);
        
        public StoneType type;
        public Sprite sprite;

        public StoneData(StoneType type, Sprite sprite)
        {
            this.type = type;
            this.sprite = sprite;
        }
    }
}