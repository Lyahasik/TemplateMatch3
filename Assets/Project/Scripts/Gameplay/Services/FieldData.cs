using ZombieVsMatch3.Gameplay.Match3;

namespace ZombieVsMatch3.Gameplay.Services
{
    public struct FieldData
    {
        public DistributorStones[] Spawns;
        public Cell[,] Cells;
            
        public int Width;
        public int Height;
    }
}