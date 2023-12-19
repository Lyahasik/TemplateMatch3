using UnityEngine;

using ZombieVsMatch3.Core.Services;
using ZombieVsMatch3.Core.Services.Factories.Gameplay;
using ZombieVsMatch3.Core.Services.Factories.UI;
using ZombieVsMatch3.Core.Services.Scene;
using ZombieVsMatch3.Gameplay.Match3;
using ZombieVsMatch3.Gameplay.Match3.Services;
using ZombieVsMatch3.Gameplay.Match3.StateMachine;
using ZombieVsMatch3.Gameplay.Match3.StateMachine.States;
using ZombieVsMatch3.UI;

namespace ZombieVsMatch3.Gameplay
{
    public class InitializerLevel : MonoBehaviour
    {
        private ISceneProviderService _sceneProviderService;
        private IUIFactory _uiFactory;
        private IGameplayFactory _gameplayFactory;
        
        private ServicesContainer _match3ServicesContainer;
        private Match3StateMachine _match3StateMachine;

        private void OnDestroy()
        {
            ClearMatch3Services();
        }

        public void Construct(ISceneProviderService sceneProviderService,
            IGameplayFactory gameplayFactory,
            IUIFactory uiFactory)
        {
            _sceneProviderService = sceneProviderService;
            _gameplayFactory = gameplayFactory;
            _uiFactory = uiFactory;
        }

        public void Initialize()
        {
            Hud hud = CreateHUD();
            CreateMatch3(hud);
        }

        private Hud CreateHUD()
        {
            Hud hud = _uiFactory.CreateHUD();
            hud.Construct(_sceneProviderService);
            hud.Initialize();

            return hud;
        }

        private void CreateMatch3(Hud hud)
        {
            RegisterMatch3Services();

            FieldMatch3 fieldMatch3 = _gameplayFactory.CreateMatch3();
            fieldMatch3.transform.SetParent(hud.transform, false);
            fieldMatch3.Initialize(
                _match3ServicesContainer.Single<IExchangeOfStonesService>(),
                _match3ServicesContainer.Single<ICellsStateCheckService>());
            
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
            _match3ServicesContainer.Register<IStonesDestructionMatch3Service>(new StonesDestructionMatch3Service(
                _match3StateMachine,
                _match3ServicesContainer.Single<ICellsStateCheckService>(),
                _match3ServicesContainer.Single<IDefiningConnectionsMatch3Service>()));
            _match3ServicesContainer.Register<IExchangeOfStonesService>(new ExchangeOfStonesService(
                _match3StateMachine,
                _match3ServicesContainer.Single<IDefiningConnectionsMatch3Service>(),
                _match3ServicesContainer.Single<ICellsStateCheckService>()));
            _match3ServicesContainer.Register<IFillingCellsMatch3Service>(new FillingCellsMatch3Service(
                _match3StateMachine,
                _match3ServicesContainer.Single<IDefiningConnectionsMatch3Service>(),
                _match3ServicesContainer.Single<ICellsStateCheckService>()));
        }

        private void ClearMatch3Services()
        {
            _match3ServicesContainer.Clear();
            
            _match3StateMachine = null;
            _match3ServicesContainer = null;
        }
    }
}