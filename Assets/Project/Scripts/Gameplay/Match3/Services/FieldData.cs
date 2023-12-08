namespace ZombieVsMatch3.Gameplay.Match3.Services
{
    public struct FieldData
    {
        public DistributorStones[] Spawns;
        public CellUpdateStone[,] Cells;
            
        public int Width;
        public int Height;
    }
}