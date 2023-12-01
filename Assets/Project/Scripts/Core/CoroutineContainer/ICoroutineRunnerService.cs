using System.Collections;
using UnityEngine;

using ZombieVsMatch3.Core.Services;

namespace ZombieVsMatch3.Core.CoroutineContainer
{
    public interface ICoroutineRunnerService : IService
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}