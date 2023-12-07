using System.Collections.Generic;
using UnityEngine;

using ZombieVsMatch3.Gameplay.Match3;

namespace ZombieVsMatch3.Gameplay.Services
{
    public class FillingCellsMatch3Service : IFillingCellsMatch3Service
    {
        private readonly IDefiningConnectionsMatch3Service _definingConnectionsMatch3Service;
        
        private FieldData _fieldData;
        
        private List<Color> types;

        public FillingCellsMatch3Service(IDefiningConnectionsMatch3Service definingConnectionsMatch3Service)
        {
            _definingConnectionsMatch3Service = definingConnectionsMatch3Service;
        }

        public void Initialize(FieldMatch3 field)
        {
            types = new();
            types.Add(Color.blue);
            types.Add(Color.green);
            types.Add(Color.red);
            
            RecordingCells(field);
            RecordingSpawns(field);
            FillingCells();
        }

        private void FillingCells()
        {
            for (int y = 0; y < _fieldData.Height; y++)
            {
                for (int x = 0; x < _fieldData.Width; x++)
                {
                    Vector2Int position = new(x, y);
                    
                    int id = Random.Range(0, types.Count);
                    while (_definingConnectionsMatch3Service.IsFormAssembled(_fieldData, position, types[id]))
                    {
                        id = Random.Range(0, types.Count);
                    }
                    
                    _fieldData.Cells[x, y].ReserveColor(types[id]);
                    _fieldData.Spawns[x].CellPuttingInQueueDelivery(_fieldData.Cells[x, y]);
                }
            }

            foreach (DistributorStones spawnStonePoint in _fieldData.Spawns)
            {
                spawnStonePoint.DistributeStones();
            }

            DebugCellsValue();
        }

        private void RecordingCells(FieldMatch3 field)
        {
            _fieldData.Width = field.Rows.Count;
            _fieldData.Height = field.Rows[0].Cells.Count;
            
            _fieldData.Cells = new Cell[_fieldData.Width, _fieldData.Height];
            for (int y = 0; y < _fieldData.Height; y++)
            {
                for (int x = 0; x < _fieldData.Width; x++)
                {
                    _fieldData.Cells[x, y] = field.Rows[x].Cells[y];
                }
            }
        }

        private void RecordingSpawns(FieldMatch3 field)
        {
            _fieldData.Spawns = new DistributorStones[_fieldData.Width];
            for (int x = 0; x < _fieldData.Width; x++)
            {
                _fieldData.Spawns[x] = field.Spawns[x];
            }
        }

        private void DebugCellsValue()
        {
            string values = string.Empty;
            
            for (int y = 0; y < _fieldData.Height; y++)
            {
                for (int x = 0; x < _fieldData.Width; x++)
                {
                    values += $"{x}.{y} {_fieldData.Cells[x, y].GetColorName()} ";
                }

                values += "\n";
            }

            Debug.Log(values);
        }
    }
}