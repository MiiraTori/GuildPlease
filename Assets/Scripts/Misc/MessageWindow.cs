using UnityEngine;
using TMPro;

public class MessageWindow : MonoBehaviour
{
    public static MessageWindow Instance;

    [Header("UI Elements")]
    public GameObject windowRoot;
    public TextMeshProUGUI messageText;

    private void Awake()
    {
        Instance = this;
        Hide(); // 起動時は非表示
    }

    public void Show(string message)
    {
        windowRoot.SetActive(true);
        messageText.text = message;
    }

    public void Hide()
    {
        windowRoot.SetActive(false);
    }

    public void ShowMessages(string[] messages, float interval = 2f)
    {
        StartCoroutine(ShowSequence(messages, interval));
    }

    private System.Collections.IEnumerator ShowSequence(string[] messages, float interval)
    {
        windowRoot.SetActive(true);

        foreach (var msg in messages)
        {
            messageText.text = msg;
            yield return new WaitForSeconds(interval);
        }

        Hide();
    }
}