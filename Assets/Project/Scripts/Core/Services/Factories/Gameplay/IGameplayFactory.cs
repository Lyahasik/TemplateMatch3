using ZombieVsMatch3.Gameplay.Match3;

namespace ZombieVsMatch3.Core.Services.Factories.Gameplay
{
    public interface IGameplayFactory : IService
    {
        public FieldMatch3 CreateMatch3();
    }
}