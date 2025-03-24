public enum AdventurerActionType
{
    None,               // 初期状態・未行動
    MoveToTarget,       // クエストの目的地へ移動
    EngageCombat,       // 戦闘（敵と交戦）
    ReturnToGuild,      // ギルドに帰還
    SubmitReport,       // クエスト報告（今後拡張）

    // --- 拡張用アクション（実装予定） ---
    Rest,               // 休憩
    Explore,            // 探索
    Gather              // 採取
}