using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class TimedInvokeExstantion
{
     public static void IvokeWithDelay(this MonoBehaviour monoBehaviour, UnityAction action, float delay)
     {
         monoBehaviour.StartCoroutine(WaitForDelay(action, delay));
     }

    private static IEnumerator WaitForDelay(UnityAction action, float delay)
    {
        yield return new WaitForSeconds(delay);

        action?.Invoke();
    }
}
