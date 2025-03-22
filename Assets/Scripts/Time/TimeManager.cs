using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    [Header("時間設定")]
    public float timeMultiplier = 60f; // ゲーム内1分 = 現実1秒（60倍速）
    private float timeAccumulator = 0f;

    [Header("現在のゲーム時間")]
    public GameTime currentTime = new GameTime();

    public delegate void TimeChangedDelegate(GameTime newTime);
    public event TimeChangedDelegate OnTimeChanged;
    public delegate void HourChangedDelegate(int newHour);
    public event HourChangedDelegate OnHourChanged;

    private int lastHour = -1; // 前回の時間を記録


    private void Awake()
    {
        // Singleton（1つだけ）
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        float scaledDeltaTime = Time.deltaTime * timeMultiplier;
        timeAccumulator += scaledDeltaTime;

        while (timeAccumulator >= 60f)
        {
            timeAccumulator -= 60f;
            currentTime.AddMinutes(1);

            OnTimeChanged?.Invoke(currentTime);

            // 時間が変わったらイベント通知
            if (currentTime.hour != lastHour)
            {
                lastHour = currentTime.hour;
                OnHourChanged?.Invoke(lastHour);
            }
        }
    }

    public GameTime GetCurrentTime()
    {
        return currentTime;
    }



    public void SetGameTime(GameTime newTime)
    {
        currentTime = newTime;
        OnTimeChanged?.Invoke(currentTime);
    }

    void PrintTime()
    {
        GameTime now = TimeManager.Instance.GetCurrentTime();
        Debug.Log("現在時刻: " + now.ToString());
    }
}