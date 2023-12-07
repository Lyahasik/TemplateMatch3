using UnityEngine;

using ZombieVsMatch3.Core.Services;

namespace ZombieVsMatch3.Gameplay.Services
{
    public interface IDefiningConnectionsMatch3Service : IService
    {
        bool IsFormAssembled(in FieldData fieldData, in Vector2Int position, in Color color);
    }
}