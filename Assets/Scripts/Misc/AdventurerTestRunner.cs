using UnityEngine;
using GameData;

public class AdventurerTestRunner : MonoBehaviour
{
    public AdventurerAIComponent aiComponent;
    public AdventurerDataSO adventurerDataSO;
    public QuestDataSO questDataSO;

    private void Start()
    {
        // 冒険者データのインスタンス作成
        AdventurerData adventurerData = adventurerDataSO.CreateAdventurerInstance();
        QuestData questData = questDataSO.CreateQuestInstance();

        // 状態の生成と設定
        AdventurerState state = new AdventurerState(adventurerData);
        state.currentQuest = questData;

        // AIコンポーネントに状態をセット
        aiComponent.state = state;

        Debug.Log("🧪 テスト開始：冒険者がクエストへ向かいます");
    }
}