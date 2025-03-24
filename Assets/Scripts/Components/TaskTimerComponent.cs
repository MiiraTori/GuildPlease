using System;
using UnityEngine;

public class TaskTimerComponent : MonoBehaviour
{
    private float remainingTime;
    private bool isRunning = false;
    private Action onTimerComplete;

    private void Update()
    {
        if (!isRunning) return;

        remainingTime -= Time.deltaTime;
        if (remainingTime <= 0f)
        {
            isRunning = false;
            onTimerComplete?.Invoke();
            onTimerComplete = null;
        }
    }

    public void StartTimer(float duration, Action onComplete)
    {
        if (duration <= 0f)
        {
            Debug.LogWarning("⏱️ 無効なタイマー時間");
            onComplete?.Invoke();
            return;
        }

        remainingTime = duration;
        onTimerComplete = onComplete;
        isRunning = true;

        Debug.Log($"⏱️ タイマー開始（{duration}秒）");
    }

    public void StopTimer()
    {
        isRunning = false;
        onTimerComplete = null;
        Debug.Log("⏹️ タイマー中断");
    }

    public bool IsRunning => isRunning;
}