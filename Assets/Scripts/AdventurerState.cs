[System.Serializable]
public class AdventurerState
{
    public AdventurerData data;

    public QuestData currentQuest;           // 🔸 現在受けているクエスト
    public string currentAreaId = "guild";   // 🔸 現在地（初期値: ギルド）
    public AdventurerActionType currentAction = AdventurerActionType.None;

    public bool isBusy = false;              // 🔸 行動中フラグ
    public int currentHP;

    public AdventurerState(AdventurerData data)
    {
        this.data = data;
        this.currentHP = data.stats.maxHP;
    }

    public void ResetState()
    {
        currentAction = AdventurerActionType.None;
        isBusy = false;
        currentAreaId = "guild";
        currentQuest = null;
    }
}