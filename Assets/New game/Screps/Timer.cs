using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    public static IEnumerator RunAfterTimer(float t, Action F) { 
        yield return new WaitForSeconds(t);
        F();
    }
    public static IEnumerator StartTimer(float t, Action<bool> F) {
        F(true);
        yield return new WaitForSeconds(t);
        F(false);
    }
}
