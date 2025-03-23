using UnityEngine;

public class TimeTestRunner : MonoBehaviour
{
    private void Start()
    {
     

        Debug.Log("✅ テスト開始：時間を早送り中");
    }

    private void Update()
    {
        // 現在時刻を定期ログ
        if (Time.frameCount % 60 == 0)
        {
            var time = TimeManager.Instance.currentTime;
            Debug.Log($"🕒 現在時間: {time.hour:00}:{time.minute:00}");
        }
    }
}