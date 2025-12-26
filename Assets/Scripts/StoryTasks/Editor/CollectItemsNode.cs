using System;
using StoryTool.Editor;
using StoryTool.Editor.BuiltInTasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[StoryTaskNodeDrawer(typeof(CollectItems))]
public class CollectItemsNode : StoryLineNode
{
    public CollectItemsNode(SerializedProperty taskProperty) : base(taskProperty)
    {
    }

    public override void OnSelected()
    {
        base.OnSelected();
        SceneView.duringSceneGui += OnSceneGUI;
    }

    public override void OnUnselected()
    {
        base.OnUnselected();
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private void OnSceneGUI(SceneView view)
    {
        Handles.color = Color.yellow;

        Handles.DrawWireDisc(
            SerializedTaskProperty.FindPropertyRelative("spawnAreaCenter").vector3Value,
            Vector3.up,
            SerializedTaskProperty.FindPropertyRelative("spawnAreaRadius").floatValue 
        );
    }
}
