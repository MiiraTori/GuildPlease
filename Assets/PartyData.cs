using System.Collections.Generic;

public class PartyData
{
    public string partyId;           // パーティのID
    public string partyName;         // パーティの名前
    public List<AdventurerData> members; // 所属する冒険者
    public string currentQuestId;    // 受注中のクエストID
    public string currentLocationId; // 現在の位置
    public bool isInCombat;          // 戦闘中かどうか

    public PartyData(string id, string name, List<AdventurerData> adventurers)
    {
        partyId = id;
        partyName = name;
        members = adventurers;
        currentQuestId = null;
        currentLocationId = "GuildHall"; // 初期位置はギルド
        isInCombat = false;
    }
}