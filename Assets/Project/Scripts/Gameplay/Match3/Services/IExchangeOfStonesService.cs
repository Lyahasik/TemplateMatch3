using ZombieVsMatch3.Core.Services;

namespace ZombieVsMatch3.Gameplay.Match3.Services
{
    public interface IExchangeOfStonesService : IService
    {
        public void HitCell(CellUpdateStone cellUpdateStone);
        public void StartSwap(bool isBack = false);
        public void Deselect();
    }
}