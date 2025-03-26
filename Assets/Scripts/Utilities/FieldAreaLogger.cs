using UnityEngine;

public class FieldAreaLogger : MonoBehaviour
{
    [SerializeField] private MonsterPopulationSystem populationSystem;

    [Tooltip("ログ出力の間隔（秒）")]
    public float logInterval = 10f;

    private void Start()
    {
        if (populationSystem == null)
        {
            populationSystem = FindObjectOfType<MonsterPopulationSystem>();
        }

        InvokeRepeating(nameof(LogAllAreaStates), 2f, logInterval);
    }

    private void LogAllAreaStates()
    {
        Debug.Log("===== フィールド状態ログ =====");
        foreach (var areaState in populationSystem.GetAllAreaStates())
        {
            Debug.Log(areaState.ToString());
        }
    }

}