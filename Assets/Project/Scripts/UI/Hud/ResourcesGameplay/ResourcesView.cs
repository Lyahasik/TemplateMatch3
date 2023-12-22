using System.Collections.Generic;
using UnityEngine;

using ZombieVsMatch3.Gameplay.Match3.Services;
using ZombieVsMatch3.Gameplay.Match3.Stones;

namespace ZombieVsMatch3.UI.Hud.ResourcesGameplay
{
    public class ResourcesView : MonoBehaviour
    {
        [SerializeField] private List<StoneIndicator> indicators;

        public void Initialize(IStoneCounterService stoneCounterService, List<StoneData> stonesData)
        {
            for (int i = 0; i < indicators.Count; i++)
            {
                indicators[i].Initialize(stonesData[i]);
                indicators[i].Subscribe(stoneCounterService);
            }
        }
    }
}
