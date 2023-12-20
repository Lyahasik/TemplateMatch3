using UnityEngine;
using UnityEngine.Serialization;

namespace ZombieVsMatch3.MainMenu.StaticData
{
    [CreateAssetMenu(fileName = "MainMenuData", menuName = "Static data/Main menu")]
    public class MainMenuStaticData : ScriptableObject
    {
        [FormerlySerializedAs("mainMenuPrefab")] public UI.MainMenuView mainMenuViewPrefab;
    }
}