namespace ZombieVsMatch3.Core.Services.Progress
{
    public interface IRecordingSavedProgress : ILoadingSavedProgress
    {
        void UpdateProgress(PlayerProgress progress);
    }
}