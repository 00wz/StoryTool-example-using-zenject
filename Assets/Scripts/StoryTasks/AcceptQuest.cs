using StoryTool.BuiltInTasks;
using UnityEngine;
using Zenject;

public class AcceptQuest : StoryLine
{
    [SerializeField]
    private InteractionTrigger questGiver;

    [SerializeField]
    private string questUniqueName;

    [SerializeField]
    private string description;
    
    UIService _uiService;
    private GameObject _questSourceMarker;

    [Inject]
    private void Init(UIService uiService)
    {
        _uiService = uiService;
    }

    protected override void ReceiveExecute()
    {
        var questSourceMarkerPrefab = Resources.Load<GameObject>("QuestSourceMarker");
        _questSourceMarker = GameObject.Instantiate(questSourceMarkerPrefab, questGiver.transform);
        questGiver.TriggerEnter += OnQuestGiverTriggered;
    }

    private void OnQuestGiverTriggered()
    {
        GameObject.Destroy(_questSourceMarker);
        questGiver.TriggerEnter -= OnQuestGiverTriggered;
        _uiService.AddOrRefreshTaskRecord(questUniqueName, description);
        FinishExecute();
    }
}