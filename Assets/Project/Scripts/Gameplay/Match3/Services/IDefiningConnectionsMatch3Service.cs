using System.Collections.Generic;
using UnityEngine;

using ZombieVsMatch3.Core.Services;
using ZombieVsMatch3.Gameplay.Match3.Stones;

namespace ZombieVsMatch3.Gameplay.Match3.Services
{
    public interface IDefiningConnectionsMatch3Service : IService
    {
        public void Initialize(in FieldData fieldData);
        public List<CellUpdateStone> GetListForms();
        public bool IsFormAssembled(in Vector2Int idPosition, in StoneType type);
        public bool IsNeighboringCells(in Vector2Int idPosition, in Vector2Int vector2Int);
    }
}