namespace ZombieVsMatch3.Core.Services.Progress
{
    public interface IRealtimeProgressService : IService
    {
        public PlayerProgress Progress { get; set; }
    }
}