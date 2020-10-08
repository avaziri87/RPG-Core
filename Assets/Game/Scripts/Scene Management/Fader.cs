using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagment
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] float fadeInTime = 0.0f;
        [SerializeField] float fadeOutTime = 0.0f;
        CanvasGroup canvasGroup;
        Coroutine currentActiveFade = null;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        public void FadeOutImmediate()
        {
            canvasGroup.alpha = 1;
        }
        public Coroutine Fade(float target, float time)
        {
            if (currentActiveFade != null) StopCoroutine(currentActiveFade);
            currentActiveFade = StartCoroutine(FadeRutine(target, time));
            return currentActiveFade;
        }
        public Coroutine FadeOut(float time)
        {
            return Fade(1, time);
        }
        public Coroutine FadeIn(float time)
        {
            return Fade(0, time);
        }
        private IEnumerator FadeRutine(float target, float time)
        {
            while (!Mathf.Approximately(canvasGroup.alpha, target))
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, Time.deltaTime / time);
                yield return null;
            }
        }
    }
}
