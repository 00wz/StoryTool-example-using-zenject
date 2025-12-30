using System.Collections;
using StoryTool.BuiltInTasks;
using UnityEngine;
using Zenject;

public class Delay : StoryLine
{
    [SerializeField]
    private float delaySeconds;

    private UIService _uiService; // any monobehavoiur

    [Inject]
    private void Init(UIService uiService)
    {
        _uiService = uiService;
    }
    protected override void ReceiveExecute()
    {
        _uiService.StartCoroutine(DelayRoutine());
    }

    private IEnumerator DelayRoutine()
    {
        yield return new WaitForSeconds(delaySeconds);
        FinishExecute();
    }
}