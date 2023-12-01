using UnityEngine;

using ZombieVsMatch3.Core.Services;
using ZombieVsMatch3.UI;

namespace ZombieVsMatch3.Core.Initialize
{
    public class GameData : MonoBehaviour
    {
        private LoadingCurtain _curtain;
        private CoroutineContainer.CoroutineContainer _coroutineContainer;
        private ServicesContainer _servicesContainer;

        public ServicesContainer ServicesContainer => _servicesContainer;
        public CoroutineContainer.CoroutineContainer CoroutineContainer => _coroutineContainer;
        public LoadingCurtain Curtain => _curtain;

        public void Construct(LoadingCurtain curtain,
            CoroutineContainer.CoroutineContainer coroutineContainer, ServicesContainer servicesContainer)
        {
            _curtain = curtain;
            _coroutineContainer = coroutineContainer;
            _servicesContainer = servicesContainer;
        }
    }
}