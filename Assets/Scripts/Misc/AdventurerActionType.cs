public enum AdventurerActionType
{
    None,               // 初期状態
    MoveToTarget,       // クエスト目的地へ移動
    EngageCombat,       // 戦闘（モンスター討伐）
    ReturnToGuild,      // ギルドに帰還
    SubmitReport,       // クエスト報告（今後拡張）

    // ここから先は拡張候補（実装予定があるなら残す）
    Rest,               // 休憩
    Explore,            // 探索
    Gather              // 採取
}