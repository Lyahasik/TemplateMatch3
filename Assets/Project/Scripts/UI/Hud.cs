using UnityEngine;
using UnityEngine.UI;

using ZombieVsMatch3.Core.Services.Scene;
using ZombieVsMatch3.Gameplay.Match3.StateMachine;
using ZombieVsMatch3.Gameplay.Match3.StateMachine.States;

namespace ZombieVsMatch3.UI
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private Button buttonMenu;

        private ISceneProviderService _sceneProviderService;
        private IMatch3StateMachine _match3StateMachine;

        public IMatch3StateMachine Match3StateMachine
        {
            set => _match3StateMachine = value;
        }

        public void Construct(ISceneProviderService sceneProviderService)
        {
            _sceneProviderService = sceneProviderService;
        }

        public void Initialize()
        {
            buttonMenu.onClick.AddListener(LoadLevel);
        }
        
        private void LoadLevel()
        {
            _match3StateMachine.Enter<StopState>();
            _sceneProviderService.LoadMainScene();
        }
    }
}
