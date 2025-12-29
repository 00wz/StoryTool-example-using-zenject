using StoryTool.BuiltInTasks;
using UnityEngine;
using Zenject;

public class KillAll : StoryLine
{
    [SerializeField]
    private Health objectPrefab;

    [SerializeField]
    private Vector3 spawnAreaCenter;

    [SerializeField]
    private float spawnAreaRadius;

    [SerializeField]
    private int spawnCount;

    private int _remainingObjectsCount = 0;
    private TargetIndicatorService _targetIndicatorService;

    [Inject]
    private void Init(TargetIndicatorService targetIndicatorService)
    {
        _targetIndicatorService = targetIndicatorService;
    }
    protected override void ReceiveExecute()
    {
        SpawnObjects();

        if (_remainingObjectsCount < 1)
        {
            FinishExecute();
        }
    }

    private void SpawnObjects()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            var objectPosition = Quaternion.Euler(90f, 0f, 0f) * (Vector3)(Random.insideUnitCircle * spawnAreaRadius) + spawnAreaCenter;
            var objectRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            var obj = GameObject.Instantiate(objectPrefab, objectPosition, objectRotation);
            _targetIndicatorService.AddTarget(obj, obj.transform);
            obj.Died += () => OnObjectDead(obj);
            _remainingObjectsCount++;
        }
    }

    private void OnObjectDead(Health obj)
    {
        _targetIndicatorService.RemoveTarget(obj);
        _remainingObjectsCount--;

        if (_remainingObjectsCount < 1) 
        {
            FinishExecute();
        }
    }
}