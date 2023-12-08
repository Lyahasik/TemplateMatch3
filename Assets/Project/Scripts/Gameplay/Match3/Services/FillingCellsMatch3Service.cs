using System.Collections.Generic;
using UnityEngine;

namespace ZombieVsMatch3.Gameplay.Match3.Services
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
            DistributeStones();
        }

        private void RecordingCells(FieldMatch3 field)
        {
            _fieldData.Width = field.Rows.Count;
            _fieldData.Height = field.Rows[0].Cells.Count;
            
            _fieldData.Cells = new CellUpdateStone[_fieldData.Width, _fieldData.Height];
            for (int y = 0; y < _fieldData.Height; y++)
            {
                for (int x = 0; x < _fieldData.Width; x++)
                {
                    _fieldData.Cells[x, y] = field.Rows[x].Cells[y];
                    _fieldData.Cells[x, y].IdPosition = new Vector2Int(x, y);
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

        private void FillingCells()
        {
            for (int y = 0; y < _fieldData.Height; y++)
            {
                for (int x = 0; x < _fieldData.Width; x++)
                {
                    CellUpdateStone cell = _fieldData.Cells[x, y];
                    
                    int id = Random.Range(0, types.Count);
                    while (_definingConnectionsMatch3Service.IsFormAssembled(_fieldData, cell.IdPosition, types[id]))
                    {
                        id = Random.Range(0, types.Count);
                    }
                    
                    cell.ReserveColor(types[id]);
                    _fieldData.Spawns[x].CellPuttingInQueueDelivery(cell);
                }
            }
        }

        private void DistributeStones()
        {
            foreach (DistributorStones spawnStonePoint in _fieldData.Spawns)
            {
                spawnStonePoint.StartDistribute();
            }
        }
    }
}