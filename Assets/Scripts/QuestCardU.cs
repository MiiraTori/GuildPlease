using UnityEngine;
using TMPro;
using GameData;

public class QuestCardUI : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI titleText;

     public QuestDataSO questData;

    /// <summary>S
    /// クエストデータを設定し、表示に反映する
    /// </summary>
    public void SetQuestData()
    {
       
        if (titleText != null)
        {
            titleText.text = questData.title;
        }
    }

    /// <summary>
    /// 保持しているクエストデータを取得
    /// </summary>
    public QuestData GetQuestData()
    {
        return questData.CreateQuestInstance();
    }
}