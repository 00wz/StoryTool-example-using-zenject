using StoryTool.BuiltInTasks;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class ShowMessage : StoryLine
{
    [SerializeField]
    private string head;

    [SerializeField]
    private string body;
    private UIService _uiService;

    [Inject]
    private void Init(UIService uiService)
    {
        _uiService = uiService;
    }

    protected override void ReceiveExecute()
    {
        _uiService.ShowMessage(head, body, FinishExecute);
    }
}