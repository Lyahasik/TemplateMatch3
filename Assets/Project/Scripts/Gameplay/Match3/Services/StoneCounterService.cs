using System;

using ZombieVsMatch3.Gameplay.Match3.Stones;

namespace ZombieVsMatch3.Gameplay.Match3.Services
{
    public class StoneCounterService : IStoneCounterService
    {
        public event Action<StoneType> OnStoneDestroy;

        public void StoneDestroy(in StoneType stoneType)
        {
            OnStoneDestroy?.Invoke(stoneType);
        }
    }
}