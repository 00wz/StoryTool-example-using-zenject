using StoryTool.BuiltInTasks;
using UnityEngine;
using Zenject;

public class CompleteQuest : StoryLine
{
    [SerializeField]
    private InteractionTrigger questGiver;

    [SerializeField]
    private string questUniqueName;

    private TargetIndicatorService _targetIndicatorService;
    private UIService _uiService;
    private GameObject _questCompletionMarker;

    [Inject]
    private void Init(TargetIndicatorService targetIndicatorService, UIService uiService)
    {
        _targetIndicatorService = targetIndicatorService;
        _uiService = uiService;
    }

    protected override void ReceiveExecute()
    {
        var questCompletionMarkerPrefab = Resources.Load<GameObject>("QuestCompletionMarker");
        _questCompletionMarker = GameObject.Instantiate(questCompletionMarkerPrefab, questGiver.transform);
        questGiver.TriggerEnter += OnQuestGiverTriggered;
        _targetIndicatorService.AddTarget(this, questGiver.transform);
    }

    private void OnQuestGiverTriggered()
    {
        GameObject.Destroy(_questCompletionMarker);
        questGiver.TriggerEnter -= OnQuestGiverTriggered;
        _uiService.RemoveTaskRecord(questUniqueName);
        _targetIndicatorService.RemoveTarget(this);
        FinishExecute();
    }
}