using System.Collections.Generic;
using UnityEngine;

using ZombieVsMatch3.Core.Services;

namespace ZombieVsMatch3.Gameplay.Match3.Services
{
    public interface IDefiningConnectionsMatch3Service : IService
    {
        public void Initialize(in FieldData fieldData);
        public List<CellUpdateStone> GetListForms();
        public bool IsFormAssembled(in Vector2Int idPosition, in Color color);
        public bool IsNeighboringCells(Vector2Int idPosition, Vector2Int vector2Int);
    }
}