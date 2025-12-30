using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class TargetIndicatorService : MonoBehaviour
{
    [SerializeField]
    private GameObject targetIndicatorPrefab;

    private Dictionary<object, (GameObject indicator, Transform target)> targetIndicators = new();
    private PlayerLifecycleService _playerLifecycleService;

    [Inject]
    private void Init(PlayerLifecycleService playerLifecycleService)
    {
        _playerLifecycleService = playerLifecycleService;
    }

    public void AddTarget(object key, Transform target)
    {
        if (targetIndicators.ContainsKey(key))
        {
            throw new System.ArgumentException($"Target with key \"{key}\" already exists");
        }

        var indicator = GameObject.Instantiate(targetIndicatorPrefab, transform);
        targetIndicators.Add(key, (indicator, target));
    }

    public void RemoveTarget(object key) 
    {
        if (!targetIndicators.ContainsKey(key))
        {
            throw new System.ArgumentException($"Target with key \"{key}\" does not exist");
        }

        GameObject.Destroy(targetIndicators[key].indicator);
        targetIndicators.Remove(key);
    }

    void Update() 
    {
        if (_playerLifecycleService?.PlayerController == null) 
        {
            return;
        }
        
        foreach(var ti in targetIndicators.Values)
        {
            ti.indicator.transform.position = _playerLifecycleService.PlayerController.transform.position;
            ti.indicator.transform.LookAt(new Vector3(ti.target.position.x, ti.indicator.transform.position.y, ti.target.position.z));
        }
    }
}
