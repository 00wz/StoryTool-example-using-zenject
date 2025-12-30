using UnityEngine;
using Zenject;

public class SceneComponentsInstaller : MonoInstaller
{
    [SerializeField]
    private Component[] _components = null;
    
    public override void InstallBindings()
    {
        foreach (var component in _components)
        {
            Container.Bind(component.GetType()).FromInstance(component);
            Container.QueueForInject(component);
        }
    }
}
