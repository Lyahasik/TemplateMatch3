using UnityEngine;
using UnityEngine.Serialization;

using ZombieVsMatch3.UI.MainMenu;

namespace ZombieVsMatch3.MainMenu.StaticData
{
    [CreateAssetMenu(fileName = "MainMenuData", menuName = "Static data/Main menu")]
    public class MainMenuStaticData : ScriptableObject
    {
        [FormerlySerializedAs("mainMenuPrefab")] public MainMenuView mainMenuViewPrefab;
    }
}