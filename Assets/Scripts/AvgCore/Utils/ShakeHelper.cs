using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AvgCore.Utils
{
    public class ShakeHelper
    {
        static readonly List<int> ActiveShakes = new List<int>(2);

        public static async UniTask Shake(Transform transform, float duration, Vector3 range, float decayStart = 1f)
        {
            // 如果我们运行多个摇晃，那么我们可以记录错误的originalPosition。现在只是不允许在同一个对象上多次摇晃
            if (!ActiveShakes.Contains(transform.GetInstanceID()))
            {
                ActiveShakes.Add(transform.GetInstanceID());

                var originalPosition = transform.localPosition;

                var randomAngle = Random.Range(0, 361);

                var decay = 0f;
                var decayFactor = Mathf.Approximately(decayStart, 1) ? 0 : 1 / (1 - decayStart);

                var elapsedTime = 0f;
                while (elapsedTime < duration)
                {
                    if (transform.gameObject != null)
                    {
                        var percentComplete = elapsedTime / duration;

                        if (percentComplete >= decayStart)
                        {
                            decay = 1 - decayFactor + decayFactor * percentComplete;
                        }

                        randomAngle += 180 + Random.Range(-60, 60);
                        var sinAngle = Mathf.Sin(randomAngle);
                        var cosAngle = Mathf.Cos(randomAngle);
                        var offset = new Vector3(cosAngle * sinAngle * range.x,
                            sinAngle * sinAngle * range.y,
                            cosAngle * range.z);
                        var target = originalPosition + (offset * (1 - decay));

                        transform.localPosition = target;
                    }

                    await UniTask.Yield();
                    elapsedTime += Time.deltaTime;
                }

                transform.localPosition = originalPosition;
                ActiveShakes.Remove(transform.GetInstanceID());
            }
        }
    }
}