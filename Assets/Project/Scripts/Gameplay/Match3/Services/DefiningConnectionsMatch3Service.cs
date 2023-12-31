using System.Collections.Generic;
using UnityEngine;

using ZombieVsMatch3.Constants;
using ZombieVsMatch3.Gameplay.Match3.Stones;

namespace ZombieVsMatch3.Gameplay.Match3.Services
{
    public class DefiningConnectionsMatch3Service : IDefiningConnectionsMatch3Service
    {
        private FieldData _fieldData;
        
        public void Initialize(in FieldData fieldData)
        {
            _fieldData = fieldData;
        }

        public List<CellUpdateStone> GetListForms()
        {
            List<CellUpdateStone> cells = null;
            
            for (int y = 0; y < _fieldData.Height; y++)
            {
                cells ??= new List<CellUpdateStone>();
                
                for (int x = 0; x < _fieldData.Width; x++)
                {
                    if (_fieldData.Cells[x, y].IsReadyForDestruction)
                        continue;
                    
                    Vector2Int idPosition = new Vector2Int(x, y);
                    
                    if (IsHorizontalConnection(idPosition, _fieldData.Cells[x, y].StoneData.type))
                        WriteStonesHorizontally(cells, idPosition);
                    
                    if (IsVerticalConnection(idPosition, _fieldData.Cells[x, y].StoneData.type))
                        WriteStonesVertical(cells, idPosition);
                }
            }
            
            return cells;
        }

        private void WriteStonesHorizontally(List<CellUpdateStone> cells, in Vector2Int idPosition)
        {
            StoneType startType = _fieldData.Cells[idPosition.x, idPosition.y].StoneData.type;
            MarkingCell(cells, _fieldData.Cells[idPosition.x, idPosition.y]);

            for (int x = idPosition.x - 1; x >= 0; x--)
            {
                if (!IsIdenticalStoneTypes(_fieldData.Cells[x, idPosition.y].StoneData.type, startType))
                    break;

                MarkingCell(cells, _fieldData.Cells[x, idPosition.y]);
            }
            
            for (int x = idPosition.x + 1; x < _fieldData.Width; x++)
            {
                if (!IsIdenticalStoneTypes(_fieldData.Cells[x, idPosition.y].StoneData.type, startType))
                    break;

                MarkingCell(cells, _fieldData.Cells[x, idPosition.y]);
            }
        }
        private void WriteStonesVertical(List<CellUpdateStone> cells, in Vector2Int idPosition)
        {
            StoneType startType = _fieldData.Cells[idPosition.x, idPosition.y].StoneData.type;
            MarkingCell(cells, _fieldData.Cells[idPosition.x, idPosition.y]);

            for (int y = idPosition.y - 1; y >= 0; y--)
            {
                if (!IsIdenticalStoneTypes(_fieldData.Cells[idPosition.x, y].StoneData.type, startType))
                    break;

                MarkingCell(cells, _fieldData.Cells[idPosition.x, y]);
            }
            
            for (int y = idPosition.y + 1; y < _fieldData.Height; y++)
            {
                if (!IsIdenticalStoneTypes(_fieldData.Cells[idPosition.x, y].StoneData.type, startType))
                    break;

                MarkingCell(cells, _fieldData.Cells[idPosition.x, y]);
            }
        }

        private void MarkingCell(List<CellUpdateStone> cells, CellUpdateStone cellMark)
        {
            cellMark.MarkingForDestruction();
            cells.Add(cellMark);
        }

        private bool IsIdenticalStoneTypes(StoneType type1, StoneType type2)
        {
            return type1 == type2;
        }

        public bool IsFormAssembled(in Vector2Int idPosition, in StoneType type)
        {
            if (IsHorizontalConnection(idPosition, type)
                || IsVerticalConnection(idPosition, type))
                return true;

            return false;
        }

        public bool IsNeighboringCells(in Vector2Int idPositionCell1, in Vector2Int idPositionCell2)
        {
            return IsNeighboringHorizontal(idPositionCell1, idPositionCell2)
                || IsNeighboringVertical(idPositionCell1, idPositionCell2);
        }

        private bool IsNeighboringHorizontal(in Vector2Int idPositionCell1, in Vector2Int idPositionCell2)
        {
            return idPositionCell1.y == idPositionCell2.y
                    && (idPositionCell1.x - 1 == idPositionCell2.x || idPositionCell1.x + 1 == idPositionCell2.x);
        }

        private bool IsNeighboringVertical(in Vector2Int idPositionCell1, in Vector2Int idPositionCell2)
        {
            return idPositionCell1.x == idPositionCell2.x
                    && (idPositionCell1.y - 1 == idPositionCell2.y || idPositionCell1.y + 1 == idPositionCell2.y);
        }

        private bool IsHorizontalConnection(in Vector2Int idPosition, in StoneType type)
        {
            int number = 1;

            for (int x = idPosition.x - 1; x >= 0; x--)
            {
                if (_fieldData.Cells[x, idPosition.y].StoneData.type != type)
                    break;
                if (number == ConstantValues.MAX_NUMBER_LACK_OF_FORM)
                    return true;

                number++;
            }
            
            for (int x = idPosition.x + 1; x < _fieldData.Width; x++)
            {
                if (_fieldData.Cells[x, idPosition.y].StoneData.type != type)
                    break;
                if (number == ConstantValues.MAX_NUMBER_LACK_OF_FORM)
                    return true;

                number++;
            }

            return false;
        }

        private bool IsVerticalConnection(in Vector2Int idPosition, in StoneType type)
        {
            int number = 1;

            for (int y = idPosition.y - 1; y >= 0; y--)
            {
                if (_fieldData.Cells[idPosition.x, y].StoneData.type != type)
                    break;
                if (number == ConstantValues.MAX_NUMBER_LACK_OF_FORM)
                    return true;

                number++;
            }
            
            for (int y = idPosition.y + 1; y < _fieldData.Height; y++)
            {
                if (_fieldData.Cells[idPosition.x, y].StoneData.type != type)
                    break;
                if (number == ConstantValues.MAX_NUMBER_LACK_OF_FORM)
                    return true;

                number++;
            }

            return false;
        }
    }
}