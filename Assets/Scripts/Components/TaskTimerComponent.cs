using System;
using UnityEngine;

public class TaskTimerComponent : MonoBehaviour
{
    public float duration;
    private float currentTime;
    private bool isRunning = false;
    // 🔸 外部から現在の状態を参照するためのプロパティ
    public bool IsRunning => isRunning;
    public Action onTimerComplete;

    /// <summary>
    /// タイマーを開始する。オプションでコールバックをセットできる。
    /// </summary>
    public void StartTimer(float time, Action callback = null)
    {
        duration = time;
        currentTime = duration;
        isRunning = true;

        if (callback != null)
        {
            onTimerComplete = callback;
        }

        Debug.Log($"⏳ タイマー開始：{duration}秒");
    }

    private void Update()
    {
        if (!isRunning) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            isRunning = false;
            Debug.Log("⏰ タイマー完了 → コールバック実行");

            try
            {
                if (onTimerComplete != null)
                {
                    onTimerComplete.Invoke();
                    Debug.Log("✅ コールバック実行成功");
                }
                else
                {
                    Debug.LogWarning("⚠️ コールバックが設定されていません");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"❌ コールバック中に例外が発生: {ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}