using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetIndicatorService : MonoBehaviour
{
    [SerializeField]
    private Transform targetIndicatorCenter;

    [SerializeField]
    private GameObject targetIndicatorPrefab;

    private Dictionary<object, (GameObject indicator, Transform target)> targetIndicators = new();

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
        foreach(var ti in targetIndicators.Values)
        {
            ti.indicator.transform.position = targetIndicatorCenter.position;
            ti.indicator.transform.LookAt(new Vector3(ti.target.position.x, ti.indicator.transform.position.y, ti.target.position.z));
        }
    }
}
