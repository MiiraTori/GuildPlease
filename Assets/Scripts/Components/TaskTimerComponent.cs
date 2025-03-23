using UnityEngine;
using System;

public class TaskTimerComponent : MonoBehaviour
{
    public float durationMinutes = 5f;  // タスクにかかる時間（ゲーム内分）
    public float remainingMinutes;      // 残り時間
    public bool isRunning = false;

    public Action OnTaskCompleted;      // タスク完了時のイベント

    private void OnEnable()
    {
        TimeManager.Instance.OnTimeChanged += OnTimeAdvanced;
    }

    private void OnDisable()
    {
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnTimeChanged -= OnTimeAdvanced;
        }
    }

    public void StartTask(float minutes, Action value)
    {
        durationMinutes = minutes;
        remainingMinutes = minutes;
        isRunning = true;
    }

    private void OnTimeAdvanced(GameTime time)
    {
        if (!isRunning) return;

        remainingMinutes -= 1f;

        if (remainingMinutes <= 0f)
        {
            isRunning = false;
            remainingMinutes = 0f;
            OnTaskCompleted?.Invoke();
        }
    }

    public bool IsTaskRunning()
    {
        return isRunning;
    }

    public float GetProgress01()
    {
        return 1f - (remainingMinutes / durationMinutes);
    }
}