using TMPro;
using UnityEngine;
using UnityEngine.UI;

using ZombieVsMatch3.Gameplay.Match3.Services;
using ZombieVsMatch3.Gameplay.Match3.Stones;

namespace ZombieVsMatch3.UI.Hud.ResourcesGameplay
{
    public class StoneIndicator : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text valueText;

        private StoneType _stoneType;
        private int _number;

        public void Initialize(in StoneData stoneData)
        {
            _stoneType = stoneData.type;
            icon.sprite = stoneData.sprite;
        }

        public void Subscribe(IStoneCounterService stoneCounterService)
        {
            stoneCounterService.OnStoneDestroy += UpValue;
        }

        private void UpValue(StoneType type)
        {
            if (_stoneType != type)
                return;
            
            _number++;
            valueText.text = _number.ToString();
        }
    }
}
