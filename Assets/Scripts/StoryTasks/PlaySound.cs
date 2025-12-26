using StoryTool.BuiltInTasks;
using UnityEngine;
using Zenject;

public class PlaySound : StoryPoint
{
    [SerializeField]
    private AudioClip clip;

    private BackgroundSoundService _backgroundSoundService;

    [Inject]
    private void Init(BackgroundSoundService backgroundSoundService) 
    {
        _backgroundSoundService = backgroundSoundService;
    }

    protected override void ReceiveExecute()
    {
        _backgroundSoundService.PlaySound(clip);
    }
}