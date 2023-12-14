using ZombieVsMatch3.Core.Services;

namespace ZombieVsMatch3.Gameplay.Match3.Services
{
    public interface IStonesDestructionMatch3Service : IService
    {
        public void TryDestroy();
    }
}