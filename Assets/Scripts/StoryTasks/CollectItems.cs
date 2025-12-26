using System.Collections.Generic;
using StoryTool.BuiltInTasks;
using UnityEngine;
using Zenject;

public class CollectItems : StoryLine
{
    [SerializeField]
    private InteractionTrigger itemPrefab;

    [SerializeField]
    private Vector3 spawnAreaCenter;

    [SerializeField]
    private float spawnAreaRadius;

    [SerializeField]
    private int spownCount;

    private int _itemsCount = 0;
    private TargetIndicatorService _targetIndicatorService;

    [Inject]
    private void Init(TargetIndicatorService targetIndicatorService)
    {
        _targetIndicatorService = targetIndicatorService;
    }

    protected override void ReceiveExecute()
    {
        SpawnItems();

        if (_itemsCount < 1) 
        {
            FinishExecute();
        }
    }

    private void SpawnItems()
    {
        for (int i = 0; i < spownCount; i++)
        {
            var itemPosition = Quaternion.Euler(90f, 0f, 0f) * (Vector3)(Random.insideUnitCircle * spawnAreaRadius) + spawnAreaCenter;
            var itemRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            var item = GameObject.Instantiate(itemPrefab, itemPosition, itemRotation);
            _targetIndicatorService.AddTarget(item, item.transform);
            item.TriggerEnter += () => OnItemCollected(item);
            _itemsCount++;
        }
    }

    private void OnItemCollected(InteractionTrigger item)
    {
        _targetIndicatorService.RemoveTarget(item);
        GameObject.Destroy(item.gameObject);
        _itemsCount--;

        if (_itemsCount < 1) 
        {
            FinishExecute();
        }
    }
}