using UnityEngine;

using ZombieVsMatch3.Core.Services;

namespace ZombieVsMatch3.Gameplay.Match3.Services
{
    public interface IDefiningConnectionsMatch3Service : IService
    {
        public bool IsFormAssembled(in FieldData fieldData, in Vector2Int idPosition, in Color color);
        public bool IsNeighboringCells(Vector2Int idPosition, Vector2Int vector2Int);
    }
}