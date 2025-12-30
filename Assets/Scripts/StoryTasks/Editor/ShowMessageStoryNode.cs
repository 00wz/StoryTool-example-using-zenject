using StoryTool.Editor;
using StoryTool.Editor.BuiltInTasks;
using UnityEditor;

[StoryTaskNodeDrawer(typeof(ShowMessage))]
public class ShowMessageStoryNode : StoryLineNode
{
    public ShowMessageStoryNode(SerializedProperty taskProperty) : base(taskProperty)
    {
    }
    protected override void BuildContent()
    {
        base.BuildContent();
        style.width = 250f;
    }
}
