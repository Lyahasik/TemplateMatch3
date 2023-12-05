namespace ZombieVsMatch3.Core.Services.Progress
{
    public interface IProgressProviderService : IService
    {
        public PlayerProgress LoadProgress();
        public void SaveProgress();
    }
}