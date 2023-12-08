using UnityEngine;

namespace ZombieVsMatch3.Gameplay.Match3.Services
{
    public class DefiningConnectionsMatch3Service : IDefiningConnectionsMatch3Service
    {
        private const int MaxNumberLackOfForm = 2;

        public bool IsFormAssembled(in FieldData fieldData, in Vector2Int idPosition, in Color color)
        {
            if (IsHorizontalConnection(fieldData, idPosition, color)
                || IsVerticalConnection(fieldData, idPosition, color))
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

        private bool IsHorizontalConnection(in FieldData fieldData, in Vector2Int idPosition, in Color color)
        {
            int number = 1;

            for (int i = idPosition.x - 1; i >= 0; i--)
            {
                if (fieldData.Cells[i, idPosition.y].Color != color)
                    break;
                if (number == MaxNumberLackOfForm)
                    return true;

                number++;
            }
            
            for (int i = idPosition.x + 1; i < fieldData.Width; i++)
            {
                if (fieldData.Cells[i, idPosition.y].Color != color)
                    break;
                if (number == MaxNumberLackOfForm)
                    return true;

                number++;
            }

            return false;
        }

        private bool IsVerticalConnection(in FieldData fieldData, in Vector2Int idPosition, in Color color)
        {
            int number = 1;

            for (int i = idPosition.y - 1; i >= 0; i--)
            {
                if (fieldData.Cells[idPosition.x, i].Color != color)
                    break;
                if (number == MaxNumberLackOfForm)
                    return true;

                number++;
            }
            
            for (int i = idPosition.y + 1; i < fieldData.Height; i++)
            {
                if (fieldData.Cells[idPosition.x, i].Color != color)
                    break;
                if (number == MaxNumberLackOfForm)
                    return true;

                number++;
            }

            return false;
        }
    }
}