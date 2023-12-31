﻿using System.Collections.Generic;

using ZombieVsMatch3.Gameplay.Match3.StateMachine;
using ZombieVsMatch3.Gameplay.Match3.StateMachine.States;

namespace ZombieVsMatch3.Gameplay.Match3.Services
{
    public class StonesDestructionMatch3Service : IStonesDestructionMatch3Service
    {
        private readonly IMatch3StateMachine _match3StateMachine;
        private readonly ICellsStateCheckService _cellsStateCheckService;
        private readonly IDefiningConnectionsMatch3Service _definingConnectionsMatch3Service;
        private readonly IStoneCounterService _stoneCounterService;

        public StonesDestructionMatch3Service(IMatch3StateMachine match3StateMachine,
            ICellsStateCheckService cellsStateCheckService,
            IDefiningConnectionsMatch3Service definingConnectionsMatch3Service,
            IStoneCounterService stoneCounterService)
        {
            _match3StateMachine = match3StateMachine;
            _cellsStateCheckService = cellsStateCheckService;
            _definingConnectionsMatch3Service = definingConnectionsMatch3Service;
            _stoneCounterService = stoneCounterService;
        }

        public void DestroyForms()
        {
            List<CellUpdateStone> cells = _definingConnectionsMatch3Service.GetListForms();
            _cellsStateCheckService.ExecuteAfterProcessing(FinishWork);
            
            foreach (CellUpdateStone cell in cells)
            {
                _stoneCounterService.StoneDestroy(cell.StoneData.type);
                cell.DestroyStone();
            }
        }

        private void FinishWork()
        {
            _match3StateMachine.Enter<FillingState>();
        }
    }
}