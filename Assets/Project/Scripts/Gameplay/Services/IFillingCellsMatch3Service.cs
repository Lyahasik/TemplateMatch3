using ZombieVsMatch3.Core.Services;
using ZombieVsMatch3.Gameplay.Match3;

namespace ZombieVsMatch3.Gameplay.Services
{
    public interface IFillingCellsMatch3Service : IService
    {
        void Initialize(FieldMatch3 field);
    }
}