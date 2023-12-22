using UnityEngine;

using ZombieVsMatch3.Core.Services;
using ZombieVsMatch3.Core.Services.Factories.Gameplay;
using ZombieVsMatch3.Core.Services.Factories.UI;
using ZombieVsMatch3.Core.Services.Scene;
using ZombieVsMatch3.Gameplay.Match3;
using ZombieVsMatch3.Gameplay.Match3.Services;
using ZombieVsMatch3.Gameplay.Match3.StateMachine;
using ZombieVsMatch3.Gameplay.Match3.StateMachine.States;
using ZombieVsMatch3.Gameplay.StaticData;
using ZombieVsMatch3.UI.Hud;

namespace ZombieVsMatch3.Gameplay
{
    public class InitializerLevel : MonoBehaviour
    {
        private ISceneProviderService _sceneProviderService;
        private IGameplayFactory _gameplayFactory;
        private IUIFactory _uiFactory;
        private LevelStaticData _levelStaticData;

        private ServicesContainer _match3ServicesContainer;
        private Match3StateMachine _match3StateMachine;

        private void OnDestroy()
        {
            ClearMatch3Services();
        }

        public void Construct(ISceneProviderService sceneProviderService,
            IGameplayFactory gameplayFactory,
            IUIFactory uiFactory,
            LevelStaticData levelStaticData)
        {
            _sceneProviderService = sceneProviderService;
            _gameplayFactory = gameplayFactory;
            _uiFactory = uiFactory;
            _levelStaticData = levelStaticData;
        }

        public void Initialize()
        {
            RegisterMatch3Services();
                
            HudView hudView = CreateHUD();
            CreateMatch3(hudView);
        }

        private HudView CreateHUD()
        {
            HudView hudView = _uiFactory.CreateHUD();
            hudView.Construct(_sceneProviderService);
            hudView.Initialize(
                _match3ServicesContainer.Single<IStoneCounterService>(),
                _levelStaticData.match3Data);

            return hudView;
        }

        private void CreateMatch3(HudView hudView)
        {
            FieldMatch3 fieldMatch3 = _gameplayFactory.CreateMatch3();
            fieldMatch3.transform.SetParent(hudView.transform, false);
            fieldMatch3.Initialize(
                _match3ServicesContainer.Single<IExchangeOfStonesService>(),
                _match3ServicesContainer.Single<ICellsStateCheckService>(),
                _levelStaticData.match3Data);
            
            _match3StateMachine.Initialize(
                _match3ServicesContainer.Single<IFillingCellsMatch3Service>(),
                fieldMatch3,
                fieldMatch3.FieldMatch3ActiveArea,
                _match3ServicesContainer.Single<IExchangeOfStonesService>(),
                _match3ServicesContainer.Single<IStonesDestructionMatch3Service>());
            _match3StateMachine.Enter<StartFillingState>();
        }

        private void RegisterMatch3Services()
        {
            _match3ServicesContainer = new ServicesContainer();
            _match3StateMachine = new Match3StateMachine();

            _match3ServicesContainer.Register<IDefiningConnectionsMatch3Service>(
                new DefiningConnectionsMatch3Service());
            _match3ServicesContainer.Register<ICellsStateCheckService>(new CellsStateCheckService());
            _match3ServicesContainer.Register<IStoneCounterService>(new StoneCounterService());
            _match3ServicesContainer.Register<IStonesDestructionMatch3Service>(new StonesDestructionMatch3Service(
                _match3StateMachine,
                _match3ServicesContainer.Single<ICellsStateCheckService>(),
                _match3ServicesContainer.Single<IDefiningConnectionsMatch3Service>(),
                _match3ServicesContainer.Single<IStoneCounterService>()));
            _match3ServicesContainer.Register<IExchangeOfStonesService>(new ExchangeOfStonesService(
                _match3StateMachine,
                _match3ServicesContainer.Single<IDefiningConnectionsMatch3Service>(),
                _match3ServicesContainer.Single<ICellsStateCheckService>()));
            _match3ServicesContainer.Register<IFillingCellsMatch3Service>(new FillingCellsMatch3Service(
                _match3StateMachine,
                _match3ServicesContainer.Single<IDefiningConnectionsMatch3Service>(),
                _match3ServicesContainer.Single<ICellsStateCheckService>(),
                _levelStaticData.match3Data.stones));
        }

        private void ClearMatch3Services()
        {
            _match3ServicesContainer.Clear();
            
            _match3StateMachine = null;
            _match3ServicesContainer = null;
        }
    }
}