using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class YieldHelper
{
    public static IEnumerator WaitForSeconds(float totalTime, bool ignoreTimeScale = false)
    {
        float curTime = 0;
        while (curTime < totalTime)
        {
            curTime += ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
            yield return null;
        }
    }

    public static IEnumerator WaifForFrame(int frameCount)
    {
        while (frameCount > 0)
        {
            frameCount--;
            yield return null;
        }
    }
}