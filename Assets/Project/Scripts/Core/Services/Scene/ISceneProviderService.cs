namespace ZombieVsMatch3.Core.Services.Scene
{
    public interface ISceneProviderService : IService
    {
        void LoadMainScene(string sceneName);
        void LoadLevel(string sceneName);
    }
}