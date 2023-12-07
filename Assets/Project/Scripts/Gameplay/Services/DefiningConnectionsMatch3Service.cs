using UnityEngine;

namespace ZombieVsMatch3.Gameplay.Services
{
    public class DefiningConnectionsMatch3Service : IDefiningConnectionsMatch3Service
    {
        private const int MaxNumberLackOfForm = 2;

        public bool IsFormAssembled(in FieldData fieldData, in Vector2Int position, in Color color)
        {
            Debug.Log(color);

            if (IsHorizontalConnection(fieldData, position, color)
                || IsVerticalConnection(fieldData, position, color))
                return true;

            return false;
        }

        private bool IsHorizontalConnection(in FieldData fieldData, in Vector2Int position, in Color color)
        {
            int number = 1;

            for (int i = position.x - 1; i >= 0; i--)
            {
                if (fieldData.Cells[i, position.y].Color != color)
                    break;
                if (number == MaxNumberLackOfForm)
                    return true;

                number++;
            }
            
            for (int i = position.x + 1; i < fieldData.Width; i++)
            {
                if (fieldData.Cells[i, position.y].Color != color)
                    break;
                if (number == MaxNumberLackOfForm)
                    return true;

                number++;
            }
            
            Debug.Log($"{position} - {number}");

            return false;
        }

        private bool IsVerticalConnection(in FieldData fieldData, in Vector2Int position, in Color color)
        {
            int number = 1;

            for (int i = position.y - 1; i >= 0; i--)
            {
                if (fieldData.Cells[position.x, i].Color != color)
                    break;
                if (number == MaxNumberLackOfForm)
                    return true;

                number++;
            }
            
            for (int i = position.y + 1; i < fieldData.Height; i++)
            {
                if (fieldData.Cells[position.x, i].Color != color)
                    break;
                if (number == MaxNumberLackOfForm)
                    return true;

                number++;
            }

            return false;
        }
    }
}