using System.Collections;
using UnityEngine;

using ZombieVsMatch3.Core.Coroutines;

namespace ZombieVsMatch3.UI
{
    public class LoadingCurtain : MonoBehaviour
    {
        private const float AlphaReductionStep = 0.03f;
        private const float StepReductionTime = 0.03f;
        
        [SerializeField] private CanvasGroup curtain;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            curtain.alpha = 1;
        }

        public void Hide(CoroutinesContainer container) =>
            container.StartCoroutine(DoFadeIn());

        private IEnumerator DoFadeIn()
        {
            while (curtain.alpha > 0)
            {
                curtain.alpha -= AlphaReductionStep;
                yield return new WaitForSeconds(StepReductionTime);
            }
      
            gameObject.SetActive(false);
        }
    }
}