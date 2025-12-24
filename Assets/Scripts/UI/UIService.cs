using UnityEngine;

public class UIService : MonoBehaviour
{
    [SerializeField]
    private MessageView messageView;

    [SerializeField]
    private TaskListView taskListView;

    public void ShowMessage(string header, string body, System.Action onClose)
    {
        Time.timeScale = 0f;
        onClose += () => { Time.timeScale = 1f; };
        messageView.ShowMessage(header, body, onClose);
    }

    public void AddOrRefreshTaskRecord(object taskKey, string description)
    {
        taskListView.AddOrRefreshTaskRecord(taskKey, description);
    }

    public void RemoveTaskRecord(object taskKey)
    {
        taskListView.RemoveTaskRecord(taskKey);
    }
}
