using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public List<PartyData> allParties = new List<PartyData>();

    public void AddParty(PartyData party)
    {
        allParties.Add(party);
    }

    public PartyData GetPartyById(string id)
    {
        var party = allParties.Find(p => p.partyId == id);
        if (party == null)
        {
            Debug.LogWarning($"パーティID {id} が見つかりません。");
        }
        return party;
    }

    public List<AdventurerData> GetPartyMembers(string partyId)
    {
        return GetPartyById(partyId)?.members;
    }

    public void RemoveParty(string id)
    {
        var party = GetPartyById(id);
        if (party != null)
        {
            allParties.Remove(party);
            Debug.Log($"パーティ {party.partyName} を削除しました。");
        }
    }
}
