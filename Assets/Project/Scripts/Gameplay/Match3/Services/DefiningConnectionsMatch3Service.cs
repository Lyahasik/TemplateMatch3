using System.Collections.Generic;
using UnityEngine;

namespace ZombieVsMatch3.Gameplay.Match3.Services
{
    public class DefiningConnectionsMatch3Service : IDefiningConnectionsMatch3Service
    {
        private const int MaxNumberLackOfForm = 2;

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
                    Vector2Int idPosition = new Vector2Int(x, y);
                    
                    if (IsHorizontalConnection(idPosition, _fieldData.Cells[x, y].Color))
                        WriteStonesHorizontally(cells, idPosition);
                    
                    if (IsVerticalConnection(idPosition, _fieldData.Cells[x, y].Color))
                        WriteStonesVertical(cells, idPosition);
                }
            }
            
            return cells;
        }

        private void WriteStonesHorizontally(List<CellUpdateStone> cells, in Vector2Int idPosition)
        {
            if (_fieldData.Cells[idPosition.x, idPosition.y].IsReadyForDestruction)
                return;
            
            Color startColor = _fieldData.Cells[idPosition.x, idPosition.y].Color;
            cells.Add(_fieldData.Cells[idPosition.x, idPosition.y]);

            for (int x = idPosition.x - 1; x >= 0; x--)
            {
                if (_fieldData.Cells[x, idPosition.y].Color != startColor)
                    break;

                _fieldData.Cells[x, idPosition.y].MarkingForDestruction();
                cells.Add(_fieldData.Cells[x, idPosition.y]);
            }
            
            for (int x = idPosition.x + 1; x < _fieldData.Width; x++)
            {
                if (_fieldData.Cells[x, idPosition.y].Color != startColor)
                    break;

                _fieldData.Cells[x, idPosition.y].MarkingForDestruction();
                cells.Add(_fieldData.Cells[x, idPosition.y]);
            }
        }
        private void WriteStonesVertical(List<CellUpdateStone> cells, in Vector2Int idPosition)
        {
            if (_fieldData.Cells[idPosition.x, idPosition.y].IsReadyForDestruction)
                return;
            
            Color startColor = _fieldData.Cells[idPosition.x, idPosition.y].Color;
            cells.Add(_fieldData.Cells[idPosition.x, idPosition.y]);

            for (int y = idPosition.y - 1; y >= 0; y--)
            {
                if (_fieldData.Cells[idPosition.x, y].Color != startColor)
                    break;

                _fieldData.Cells[idPosition.x, y].MarkingForDestruction();
                cells.Add(_fieldData.Cells[idPosition.x, y]);
            }
            
            for (int y = idPosition.x + 1; y < _fieldData.Height; y++)
            {
                if (_fieldData.Cells[idPosition.x, y].Color != startColor)
                    break;

                _fieldData.Cells[idPosition.x, y].MarkingForDestruction();
                cells.Add(_fieldData.Cells[idPosition.x, y]);
            }
        }

        public bool IsFormAssembled(in Vector2Int idPosition, in Color color)
        {
            if (IsHorizontalConnection(idPosition, color)
                || IsVerticalConnection(idPosition, color))
                return true;

            return false;
        }

        public bool IsNeighboringCells(Vector2Int idPositionCell1, Vector2Int idPositionCell2)
        {
            return IsNeighboringHorizontal(idPositionCell1, idPositionCell2)
                || IsNeighboringVertical(idPositionCell1, idPositionCell2);
        }

        private bool IsNeighboringHorizontal(Vector2Int idPositionCell1, Vector2Int idPositionCell2)
        {
            return idPositionCell1.y == idPositionCell2.y
                    && (idPositionCell1.x - 1 == idPositionCell2.x || idPositionCell1.x + 1 == idPositionCell2.x);
        }

        private bool IsNeighboringVertical(Vector2Int idPositionCell1, Vector2Int idPositionCell2)
        {
            return idPositionCell1.x == idPositionCell2.x
                    && (idPositionCell1.y - 1 == idPositionCell2.y || idPositionCell1.y + 1 == idPositionCell2.y);
        }

        private bool IsHorizontalConnection(in Vector2Int idPosition, in Color color)
        {
            int number = 1;

            for (int x = idPosition.x - 1; x >= 0; x--)
            {
                if (_fieldData.Cells[x, idPosition.y].Color != color)
                    break;
                if (number == MaxNumberLackOfForm)
                    return true;

                number++;
            }
            
            for (int x = idPosition.x + 1; x < _fieldData.Width; x++)
            {
                if (_fieldData.Cells[x, idPosition.y].Color != color)
                    break;
                if (number == MaxNumberLackOfForm)
                    return true;

                number++;
            }

            return false;
        }

        private bool IsVerticalConnection(in Vector2Int idPosition, in Color color)
        {
            int number = 1;

            for (int y = idPosition.y - 1; y >= 0; y--)
            {
                if (_fieldData.Cells[idPosition.x, y].Color != color)
                    break;
                if (number == MaxNumberLackOfForm)
                    return true;

                number++;
            }
            
            for (int y = idPosition.y + 1; y < _fieldData.Height; y++)
            {
                if (_fieldData.Cells[idPosition.x, y].Color != color)
                    break;
                if (number == MaxNumberLackOfForm)
                    return true;

                number++;
            }

            return false;
        }
    }
}