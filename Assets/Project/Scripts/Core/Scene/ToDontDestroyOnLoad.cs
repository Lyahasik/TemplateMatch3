using UnityEngine;

namespace ZombieVsMatch3.Core.Scene
{
    public class ToDontDestroyOnLoad : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
