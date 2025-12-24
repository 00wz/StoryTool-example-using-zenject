using UnityEngine;
using Zenject;

public class StoryToolInstaller : MonoInstaller
{
    [SerializeField]
    private StoryTool.Runtime.StoryTool storyTool;
    
    public override void InstallBindings()
    {
        foreach (var task in storyTool.StoryTasks)
        {
            Container.QueueForInject(task);
        }
    }
}