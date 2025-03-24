using UnityEngine;
using UnityEngine.EventSystems;
using GameData;

[RequireComponent(typeof(AdventurerAIComponent))]
public class AdventurerDropHandler : MonoBehaviour, IDropHandler
{
    private AdventurerAIComponent aiComponent;

    private void Awake()
    {
        aiComponent = GetComponent<AdventurerAIComponent>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObj = eventData.pointerDrag;
        if (droppedObj == null) return;

        // QuestCardUI がアタッチされているかチェック
        QuestCardUI cardUI = droppedObj.GetComponent<QuestCardUI>();
        if (cardUI == null) return;

        QuestData quest = cardUI.GetQuestData();
        if (quest == null) return;

        // クエストを冒険者に割り当て
        aiComponent.AssignQuest(quest);
        Debug.Log($"🧭 冒険者にクエスト「{quest.title}」を渡しました！");

    }
}