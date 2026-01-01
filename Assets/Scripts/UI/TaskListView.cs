using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskListView : MonoBehaviour
{
    [SerializeField]
    private Text taskRecordPrefab;

    private Dictionary<object, Text> taskRecords = new Dictionary<object, Text>();
    
    public void AddOrRefreshTaskRecord(object taskKey, string description)
    {
        if (taskRecords.ContainsKey(taskKey))
        {
            taskRecords[taskKey].text = description;
        }
        else
        {
            Text newRecord = Instantiate(taskRecordPrefab, transform);
            newRecord.text = description;
            taskRecords[taskKey] = newRecord;
        }
    }

    public void RemoveTaskRecord(object taskKey)
    {
        if (taskRecords.ContainsKey(taskKey))
        {
            Destroy(taskRecords[taskKey].gameObject);
            taskRecords.Remove(taskKey);
        }
    }
}
