using ZombieVsMatch3.Core.Services;

namespace ZombieVsMatch3.Gameplay.Match3.Services
{
    public interface IExchangeOfStonesService : IService
    {
        void HitCell(CellUpdateStone cellUpdateStone);
        void Deselect();
    }
}