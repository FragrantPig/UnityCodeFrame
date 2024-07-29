
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Base
{
    public class TimeManager : MonoSingleton<TimeManager>, IMonoSingleton
    {
        public override bool IsDontDestroy => true;

        public Coroutine StartCountDown(int nTotalTime,UnityAction<int> nUpdateCallback = null, UnityAction nEndCallback = null)
        {
            return StartCoroutine(_CountDown(nTotalTime, nUpdateCallback, nEndCallback));
        }

        public void EndCountDown(Coroutine n)
        {
            StopCoroutine(n);
        }

        private IEnumerator _CountDown(int nTotalTime,UnityAction<int> nUpdateCallback = null, UnityAction nEndCallback = null)
        {
            int nCountDownTime = nTotalTime;
            while(nCountDownTime >= 0)
            {
                yield return new WaitForSeconds(1f);
                nCountDownTime--;
                nUpdateCallback?.Invoke(nCountDownTime);
            }
            nEndCallback?.Invoke();
        }

        public Coroutine RunCoroutine(IEnumerator nCoroutine)
        {
            return StartCoroutine(nCoroutine);
        }

        public void KillCoroutine(Coroutine nCoroutine)
        {
            StopCoroutine(nCoroutine);
        }
    }
}