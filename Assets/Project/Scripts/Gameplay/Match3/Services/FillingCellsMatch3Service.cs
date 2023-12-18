using System.Collections.Generic;
using UnityEngine;

using ZombieVsMatch3.Gameplay.Match3.StateMachine;
using ZombieVsMatch3.Gameplay.Match3.StateMachine.States;
using Random = UnityEngine.Random;

namespace ZombieVsMatch3.Gameplay.Match3.Services
{
    public class FillingCellsMatch3Service : IFillingCellsMatch3Service
    {
        private readonly IMatch3StateMachine _match3StateMachine;
        private readonly IDefiningConnectionsMatch3Service _definingConnectionsMatch3Service;
        private readonly ICellsStateCheckService _cellsStateCheckService;

        private FieldData _fieldData;

        private List<Color> types;

        public FillingCellsMatch3Service(IMatch3StateMachine match3StateMachine,
            IDefiningConnectionsMatch3Service definingConnectionsMatch3Service,
            ICellsStateCheckService cellsStateCheckService)
        {
            _match3StateMachine = match3StateMachine;
            _definingConnectionsMatch3Service = definingConnectionsMatch3Service;
            _cellsStateCheckService = cellsStateCheckService;
        }

        public void Initialize()
        {
            types = new();
            types.Add(Color.blue);
            types.Add(Color.green);
            types.Add(Color.red);
        }

        public void FirstFilling(FieldMatch3 field)
        {
            _cellsStateCheckService.ExecuteAfterProcessing(FinishWork);
            
            RecordingCells(field);
            RecordingSpawns(field);
            _cellsStateCheckService.Initialize(_fieldData);
            _definingConnectionsMatch3Service.Initialize(_fieldData);
            FillingEmptyCells(true);
            DistributeNewStones();
        }

        public void DistributeStones()
        {
            _cellsStateCheckService.ExecuteAfterProcessing(FinishWork);
            
            for (int x = 0; x < _fieldData.Width; x++)
            {
                for (int y = 0; y < _fieldData.Height; y++)
                {
                    if (_fieldData.Cells[x, y].Color != Color.clear)
                        continue;
                    
                    if (y < _fieldData.Height - 1)
                    {
                        for (int tempY = y + 1; tempY < _fieldData.Height; tempY++)
                        {
                            if (_fieldData.Cells[x, tempY].Color == Color.clear)
                                continue;
                                
                            _fieldData.Cells[x, y].TakeStone(_fieldData.Cells[x, tempY].transform.position,
                                _fieldData.Cells[x, tempY].Color);
                            _fieldData.Cells[x, tempY].SetColor(Color.clear);

                            break;
                        }
                    }
                }
            }
            
            FillingEmptyCells();
            DistributeNewStones();
        }

        private void FinishWork()
        {
            for (int y = 0; y < _fieldData.Height; y++)
            {
                for (int x = 0; x < _fieldData.Width; x++)
                {
                    CellUpdateStone cell = _fieldData.Cells[x, y];
                    Vector2Int position = new Vector2Int(x, y);

                    if (_definingConnectionsMatch3Service.IsFormAssembled(in position, cell.Color))
                    {
                        _match3StateMachine.Enter<DestructionState>();
                        return;
                    }
                }
            }

            _match3StateMachine.Enter<SelectionState>();
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
                    _fieldData.Cells[x, y].DestroyStone();
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

        private void FillingEmptyCells(bool isFirstTime = false)
        {
            for (int y = 0; y < _fieldData.Height; y++)
            {
                for (int x = 0; x < _fieldData.Width; x++)
                {
                    CellUpdateStone cell = _fieldData.Cells[x, y];
                    
                    if (cell.Color != Color.clear)
                        continue;
                    
                    int id = Random.Range(0, types.Count);

                    if (isFirstTime)
                        id = GetIdTakingIntoAccountForms(cell, ref id);
                    
                    cell.SetColor(types[id]);
                    _fieldData.Spawns[x].CellPuttingInQueueDelivery(cell);
                }
            }
        }

        private int GetIdTakingIntoAccountForms(CellUpdateStone cell, ref int id)
        {
            while (_definingConnectionsMatch3Service.IsFormAssembled(cell.IdPosition, types[id]))
                id = Random.Range(0, types.Count);

            return id;
        }

        private void DistributeNewStones()
        {
            foreach (DistributorStones spawnStonePoint in _fieldData.Spawns)
            {
                spawnStonePoint.StartDistribute();
            }
        }
    }
}