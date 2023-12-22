using UnityEngine;

using ZombieVsMatch3.Core.Coroutines;
using ZombieVsMatch3.Core.Services;
using ZombieVsMatch3.UI.Loading;

namespace ZombieVsMatch3.Core.Initialize
{
    public class GameData : MonoBehaviour
    {
        private LoadingCurtain _curtain;
        private CoroutinesContainer _coroutinesContainer;
        private ServicesContainer _servicesContainer;

        public ServicesContainer ServicesContainer => _servicesContainer;
        public CoroutinesContainer CoroutinesContainer => _coroutinesContainer;
        public LoadingCurtain Curtain => _curtain;

        public void Construct(LoadingCurtain curtain,
            CoroutinesContainer coroutinesContainer, ServicesContainer servicesContainer)
        {
            _curtain = curtain;
            _coroutinesContainer = coroutinesContainer;
            _servicesContainer = servicesContainer;
        }
    }
}