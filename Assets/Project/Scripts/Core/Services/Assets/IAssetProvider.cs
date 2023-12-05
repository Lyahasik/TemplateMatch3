using UnityEngine;

namespace ZombieVsMatch3.Core.Services.Assets
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path, Vector3 at);
        GameObject Instantiate(string path);
    }
}