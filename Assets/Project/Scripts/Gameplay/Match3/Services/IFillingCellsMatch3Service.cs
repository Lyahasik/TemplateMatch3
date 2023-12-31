using ZombieVsMatch3.Core.Services;

namespace ZombieVsMatch3.Gameplay.Match3.Services
{
    public interface IFillingCellsMatch3Service : IService
    {
        public void FirstFilling(FieldMatch3 field);
        public void DistributeStones();
    }
}