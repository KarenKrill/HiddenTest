using System;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace KarenKrill.Utilities
{
    public static class TimerUtility
    {
        public static async UniTask StartCountdownAsync(float duration,
            float interval,
            Action<float> tickAction,
            Action completedAction,
            CancellationToken cancellationToken = default)
        {
            float timeLeft = duration;
            while (timeLeft > float.Epsilon && !cancellationToken.IsCancellationRequested)
            {
                tickAction?.Invoke(timeLeft);
                await UniTask.Delay(TimeSpan.FromSeconds(interval), cancellationToken: cancellationToken);
                timeLeft = Mathf.Max(0f, timeLeft - interval);
            }
            if (!cancellationToken.IsCancellationRequested)
            {
                completedAction?.Invoke();
            }
        }
    }
}
