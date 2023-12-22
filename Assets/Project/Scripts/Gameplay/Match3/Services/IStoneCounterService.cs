using System;

using ZombieVsMatch3.Core.Services;
using ZombieVsMatch3.Gameplay.Match3.Stones;

namespace ZombieVsMatch3.Gameplay.Match3.Services
{
    public interface IStoneCounterService : IService
    {
        event Action<StoneType> OnStoneDestroy;
        void StoneDestroy(in StoneType stoneType);
    }
}