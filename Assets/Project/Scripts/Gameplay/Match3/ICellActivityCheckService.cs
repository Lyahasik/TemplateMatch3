using ZombieVsMatch3.Core.Services;
using ZombieVsMatch3.Gameplay.Match3.Services;

namespace ZombieVsMatch3.Gameplay.Match3
{
    public interface ICellActivityCheckService : IService
    {
        public void Initialize(FieldData fieldData);
        public bool IsAllUnlocked();
    }
}